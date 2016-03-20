using UnityEngine;
using System.Collections;
using EventsSystem;

public class GameCameraController 
	: MonoBehaviour 
	, IHandleEvent<EventOnButtonClicked>
{
	[SerializeField]
	private float _startSpeed = 1f;

	[SerializeField]
	private float _speedMinValue = 1f;

	[SerializeField]
	private float _speedMaxValue = 10f;

	[SerializeField]
	private float _speedCoef = 5f;

	[SerializeField]
	private float _rotateAnimationTime = 0.3f;

	private float _actualSpeed;
	private float _actualAngle;

	void Start()
	{
		SetActualSpeed(_startSpeed);
	}

	void Update() 
	{
		transform.position += transform.forward * Time.deltaTime * _actualSpeed * _speedCoef;

		EventBus.Instance.Post<EventCameraMoved>(this, new EventCameraMoved(transform.position));
	}

	void OnEnable()
	{
		EventBus.Instance.Subscribe<EventOnButtonClicked>(this);
	}

	void OnDisable()
	{
		if (EventBus.Exists)
		{
			EventBus.Instance.Unsubscribe<EventOnButtonClicked>(this);
		}
	}

	#region IHandleEvent implementation

	public void Handle(object sender, EventOnButtonClicked data)
	{
		if (data.Type == UIButtonType.Up)
			IncreaseSpeed();
		else if (data.Type == UIButtonType.Down)
			DecreaseSpeed();
		else if (data.Type == UIButtonType.Left)
			RotateLeft();
		else if (data.Type == UIButtonType.Right)
			RotateRight();
	}

	#endregion

	private void IncreaseSpeed()
	{
		float speed = _actualSpeed + 1;
		if (speed <= _speedMaxValue)
			SetActualSpeed(speed);
	}

	private void DecreaseSpeed()
	{
		float speed = _actualSpeed - 1;
		if (speed >= _speedMinValue)
			SetActualSpeed(speed);
	}

	private void RotateLeft()
	{
		if (LeanTween.isTweening(gameObject) == false)
		{
			_actualAngle -= 90f;
			LeanTween.rotateY(gameObject, _actualAngle, _rotateAnimationTime);
		}
	}

	private void RotateRight()
	{
		if (LeanTween.isTweening(gameObject) == false)
		{
			_actualAngle += 90f;
			LeanTween.rotateY(gameObject, _actualAngle, _rotateAnimationTime);
		}
	}

	private void SetActualSpeed(float speed)
	{
		_actualSpeed = speed;

		EventBus.Instance.Post<EventSpeedChanged>(this, new EventSpeedChanged(speed));
	}
}
