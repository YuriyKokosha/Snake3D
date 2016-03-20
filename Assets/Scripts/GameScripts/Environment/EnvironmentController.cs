using UnityEngine;
using System.Collections;
using LevelEditor;

namespace Environment
{
	public class EnvironmentController : MonoSingleton<EnvironmentController>
	{
		public const string kObjectResourcesPath = "Environment";
		public const string kEnvironmentTag = "Environment";

		public void ConstructEnvironment(LevelBuilderData data)
		{
			var prefabs = Resources.LoadAll(kObjectResourcesPath);

			for (int i = 0; i < data.DecorationsCount; i++)
			{
				int prefabNumber = Random.Range(0, prefabs.Length);

				int positionX = Random.Range(0, (int)data.GridSize.x) - (int)data.GridSize.x/2;
				int positionZ = Random.Range(0, (int)data.GridSize.y) - (int)data.GridSize.y/2;
				int positionY = 0;

				var prefab = (GameObject) prefabs[prefabNumber];
				var gameObject = GameObject.Instantiate(prefab) as GameObject;
				gameObject.transform.position = new Vector3(positionX, positionY, positionZ);
				gameObject.transform.parent = transform.parent;
				gameObject.transform.localScale = Vector3.one * 2f;		// TODO change objects scale from config
				gameObject.tag = kEnvironmentTag;
			}
		}
	}
}
