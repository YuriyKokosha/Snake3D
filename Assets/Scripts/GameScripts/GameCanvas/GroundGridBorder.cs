using UnityEngine;
using System.Collections;

public class GroundGridBorder : MonoBehaviour 
{
	[SerializeField]
	private GroundGridBorderPresenter _presenter;
	public GroundGridBorderPresenter Presenter { get { return _presenter; } }

	[SerializeField]
	private GroundBorderCollisionComponent _collisionComponent;
	public GroundBorderCollisionComponent CollisionComponent { get { return _collisionComponent; } }

	public void OnGroundRealSizeUpdated(Vector2 groundRealSize)
	{
		float size = 0f;
		var stretchType = _presenter.StretchType;

		if (stretchType == BorderStretchType.Width)
			size = groundRealSize.x;
		else if (stretchType == BorderStretchType.Height)
			size = groundRealSize.y;

		_collisionComponent.SetColliderSize(size, stretchType);
	}

	public void SetBorderVisibility(bool value)
	{
		_presenter.SetVisibility(value);
	}
}
