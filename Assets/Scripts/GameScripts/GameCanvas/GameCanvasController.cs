using UnityEngine;
using System.Collections;
using LevelEditor;

namespace GameCanvas
{
	public class GameCanvasController : MonoBehaviour 
	{
		public const string kPrefabResourcesPath = "GameCanvas/GameCanvas";
		public const string kPrefabName = "GameCanvas";

		[SerializeField] 
		private Vector3 _defaultPosition = new Vector3(0.5f,0f,0.5f);

		[SerializeField]
		private GroundGridPresenter _groundGridPresenter;

		[SerializeField]
		private GroundGridBorder[] _groundGridBorders;

		[SerializeField]
		private Transform _transform;

		public void ConstructGameCanvas(LevelBuilderData data)
		{
			if (_transform == null)
				_transform = transform;

			_transform.position = _defaultPosition;

			_groundGridPresenter.SetGridSize(data.GridSize);

			Vector2 groundRealSize = _groundGridPresenter.GetGroundRealSize();
			foreach (var groundGridBorder in _groundGridBorders)
			{
				groundGridBorder.OnGroundRealSizeUpdated(groundRealSize);
				groundGridBorder.SetBorderVisibility(data.ShouldGenerateBorder);
			}
		}
	}
}
