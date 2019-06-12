using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mochineko.ScriptCreator
{
	internal class ScriptCreatorWindow : EditorWindow
	{
		[SerializeField, TextArea]
		private string header;
		public string Header => header;
		[SerializeField]
		private string[] references;
		public string[] References => references;
		[SerializeField]
		private string nameSpace;
		public string NameSpace => nameSpace;
		[SerializeField]
		private string accessibility;
		public string Accessibility => accessibility;
		[SerializeField]
		private string objectType;
		public string ObjectType => objectType;
		[SerializeField, TextArea]
		private string statement;
		public string Statement => statement;


		private SerializedObject serializedObject;


		[MenuItem("Mochineko/Script Creator")]
		public static void Open()
		{
			GetWindow<ScriptCreatorWindow>("Script Creator");
		}


		private void OnEnable()
		{
			serializedObject = new SerializedObject(this);
		}

		private void OnGUI()
		{
			if (serializedObject == null)
				return;

			serializedObject.Update();

			EditorGUI.BeginChangeCheck();
			{
				EditorGUILayout.Space();

				EditorGUILayout.BeginVertical(GUI.skin.box);
				{
					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(header)), true);

					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(references)), true);

					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(nameSpace)));

					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(accessibility)));

					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(objectType)));

					EditorGUILayout.Space();

					EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(statement)));

					EditorGUILayout.Space();
				}
				EditorGUILayout.EndVertical();
			}
			if (EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
		}
	}
}
