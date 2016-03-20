using UnityEngine;
using System.Collections;

public class GroundBorderCollisionComponent : MonoBehaviour 
{
	[SerializeField]
	private BoxCollider _collider;

	[SerializeField]
	private Vector3 _colliderDefaultSize = new Vector3(200,200,1000);

	public void SetColliderSize(float borderSize, BorderStretchType type)
	{
		Vector3 actualSize = _colliderDefaultSize;

		switch (type)
		{
			case BorderStretchType.Width:
			{
				actualSize.x = borderSize;
			}
			break;
			case BorderStretchType.Height:
			{
				actualSize.y = borderSize;
			}
			break;
		}

		_collider.size = actualSize;
	}
}
