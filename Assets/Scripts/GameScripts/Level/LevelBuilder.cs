using UnityEngine;
using System.Collections;
using GameCanvas;
using Environment;

namespace LevelEditor
{
	public static class LevelBuilder
	{
		public const string kGameObjectsTag = "Environment";

		public static void Construct(LevelBuilderData data)
		{
			var gameCanvasController = GameObject.FindObjectOfType<GameCanvasController>();
			if (gameCanvasController == null)
			{
				var prefab = Resources.Load(GameCanvasController.kPrefabResourcesPath);
				var gameObject = GameObject.Instantiate(prefab) as GameObject;
				gameObject.name = GameCanvasController.kPrefabName;
				gameObject.tag = kGameObjectsTag;
				gameCanvasController = gameObject.GetComponent<GameCanvasController>();
			}
				
			gameCanvasController.ConstructGameCanvas(data);

			EnvironmentController.Instance.ConstructEnvironment(data);
		}

		public static void ClearScene()
		{
			var gameObjects = GameObject.FindObjectsOfType<GameObject>();
			foreach (var gameObject in gameObjects)
				if (gameObject.tag.Equals(kGameObjectsTag))
					GameObject.DestroyImmediate(gameObject);
		}
	}

	public struct LevelBuilderData
	{
		public Vector2 GridSize;
		public int DecorationsCount;
		public bool ShouldGenerateBorder;
	}
}