using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Mochineko.ScriptCreator
{
	internal class ScriptCreatorMenu
	{
		private const string templateFileRelativePath = "Mochineko/ScriptCreator/81-C# CSharpScript.cs.txt";
		private const string meunPath = "Assets/Create/Mochineko/C Sharp Script";
		private const string csIconName = "cs Script Icon";
		private const string defaultScriptName = "NewCSharpScript.cs";

		protected static string TemplateFilePath
			=> Path.Combine(
				Application.dataPath,
				templateFileRelativePath
				);

		protected static Texture2D CsIcon
			=> EditorGUIUtility.IconContent(csIconName).image as Texture2D;

		protected static ScriptCreator GetCreator
			=> ScriptableObject.CreateInstance<ScriptCreator>();


		[MenuItem(meunPath, false, 0)]
		public static void CreateCsScript()
		{
			var creator = GetCreator;
			var window = ScriptCreatorWindow.GetWindow<ScriptCreatorWindow>("Script Creator");

			creator.Header = window.Header;
			creator.References = window.References;
			creator.NameSpace = window.NameSpace;
			creator.Accessibility = window.Accessibility;
			creator.ObjectType = window.ObjectType;
			creator.Statement = window.Statement;

			ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
				0,
				creator,
				defaultScriptName,
				CsIcon,
				TemplateFilePath
			);
		}
	}
}
