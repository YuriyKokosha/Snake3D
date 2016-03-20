using UnityEngine;
using System.Collections;
using UnityEditor;

namespace LevelEditor
{
	public class LevelBuilderEditorWindow : EditorWindow 
	{
		private LevelBuilderData _data = new LevelBuilderData();

		[MenuItem("LevelEditor/Builder")]
		private static void ShowWindow()
		{
			var window = (LevelBuilderEditorWindow)EditorWindow.GetWindow(typeof(LevelBuilderEditorWindow));
			window.Show();
		}

		private void OnGUI()
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.Space();

			_data.ShouldGenerateBorder = EditorGUILayout.Toggle("ShouldGenerateBorder: ", _data.ShouldGenerateBorder);
			_data.DecorationsCount = EditorGUILayout.IntField("DecorationsCount: ", _data.DecorationsCount);
			_data.GridSize = EditorGUILayout.Vector2Field("LevelGridSize: ", _data.GridSize);

			EditorGUILayout.Space();

			bool isConstructPressed = GUILayout.Button("Construct", GUILayout.MinWidth(250f));
			bool isClearPressed = GUILayout.Button("Clear Scene", GUILayout.MinWidth(250f));

			EditorGUILayout.EndVertical();

			if (isConstructPressed)
			{
				if (IsInputValuesCorrect())
				{
					LevelBuilder.Construct(_data);
				}
			}
			else if (isClearPressed)
			{
				LevelBuilder.ClearScene();
			}
		}

		private bool IsInputValuesCorrect()
		{
			if (_data.DecorationsCount < 0)
			{
				string message = "Cannot construct with Decorations Count == " + _data.DecorationsCount + ". Please enter value >= 0";
				EditorUtility.DisplayDialog("Error", message, "OK");
				return false;
			}
			else if (_data.GridSize.x < 10 || _data.GridSize.y < 10)
			{
				string message = "Cannot construct with Level Grid Size == " + _data.GridSize.ToString() + ". Please enter values >= 10";
				EditorUtility.DisplayDialog("Error", message, "OK");
				return false;
			}

			return true;
		}
	}
}
