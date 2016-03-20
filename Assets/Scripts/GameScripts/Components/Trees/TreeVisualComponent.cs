using UnityEngine;
using System.Collections;
using EventsSystem;

public class TreeVisualComponent 
	: MonoBehaviour
	, IHandleEvent<EventCameraMoved>
{
	[Header("Visibility Distance")]
	[SerializeField]
	private float _invisibleDistanceMin = 20.0f;

	[SerializeField]
	private float _visibleDistanceMin = 30.0f;

	[SerializeField]
	private float _visibleDistanceMax = 200.0f;

	[Header("Transparent Values")]
	[SerializeField]
	private float _alphaMin = 0f;

	[SerializeField]
	private float _alphaMax = 1f;

	[Header("Components")]
	[SerializeField]
	private Renderer _renderer;

	private Transform _transform;

	private Material _sharedMaterial;
	private Material _material;

	private Color _color;
	private float _alpha;

	void Awake()
	{
		if (_renderer == null)
			_renderer = GetComponent<Renderer>();

		if (_transform == null)
			_transform = transform;

		_sharedMaterial = _renderer.sharedMaterial;
		_material = _renderer.material;

		_color = _sharedMaterial.color;
		_alpha = _color.a;

		_renderer.material = _sharedMaterial;
	}

	void OnEnable()
	{
		EventBus.Instance.Subscribe<EventCameraMoved>(this);
	}

	void OnDisable()
	{
		if (EventBus.Exists)
			EventBus.Instance.Unsubscribe<EventCameraMoved>(this);
	}

	#region IHandleEvent implementation

	public void Handle(object sender, EventCameraMoved data)
	{
		OnCameraMoved(data.Position);
	}

	#endregion

	private void OnCameraMoved(Vector3 cameraPosition)
	{
		if (IsCameraNear(cameraPosition))
		{
			// calculate distance to camera
			float distance = GetDistanceToCamera(cameraPosition);

			if (distance > _visibleDistanceMin) 
			{
				SetAlpha(_alphaMax);
			}
			else if (distance < _invisibleDistanceMin) 
			{
				SetAlpha(_alphaMin);
			}
			else 
			{
				float distanceCoef = (distance - _invisibleDistanceMin) / (_visibleDistanceMin - _invisibleDistanceMin);
				SetAlpha(_alphaMin + distanceCoef * (_alphaMax - _alphaMin));
			}
		}
		else if (IsCameraFar(cameraPosition))
		{
			// TODO animated hide
			SetAlpha(_alphaMin);
		}
		else
		{
			// TODO animated hide
			SetAlpha(_alphaMax);
		}
	}

	private void SetAlpha(float value) 
	{
		if (_alpha != value) 
		{
			_alpha = value;
			_color.a = _alpha;

			if (_alpha == 1f)
			{
				_renderer.material = _sharedMaterial;
			}
			else
			{
				_material.SetColor("_Color", _color);
				_renderer.material = _material;
			}
		}
	}

	private float GetDistanceToCamera(Vector3 cameraPosition) 
	{
		float distance = Vector3.Distance(_transform.position, cameraPosition);
		return distance;
	}

	private bool IsCameraNear(Vector3 cameraPosition) 
	{
		if (Mathf.Abs (_transform.position.x - cameraPosition.x) > _visibleDistanceMin)
			return false;
		if (Mathf.Abs (_transform.position.y - cameraPosition.y) > _visibleDistanceMin)
			return false;
		if (Mathf.Abs (_transform.position.z - cameraPosition.z) > _visibleDistanceMin)
			return false;

		return true;
	}

	private bool IsCameraFar(Vector3 cameraPosition) 
	{
		if (Mathf.Abs (_transform.position.x - cameraPosition.x) < _visibleDistanceMax)
			return false;
		if (Mathf.Abs (_transform.position.y - cameraPosition.y) < _visibleDistanceMax)
			return false;
		if (Mathf.Abs (_transform.position.z - cameraPosition.z) < _visibleDistanceMax)
			return false;

		return true;
	}
}
