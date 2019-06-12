using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using System.Text;

namespace Mochineko.ScriptCreator
{
	public class ScriptCreator : EndNameEditAction
	{
		public string Header { private get; set; } = "// Header";
		public string[] References { private get; set; } = new string[0];
		public string NameSpace { private get; set; } = "";
		public string Accessibility { private get; set; } = "public";
		public string ObjectType { private get; set; } = "class";
		public string Statement { private get; set; } = "";


		//internal static readonly string[] Accessibilities = new string[]
		//{
		//	"public",
		//	"internal",
		//	"public partial",
		//	"internal partial",

		//};

		//static readonly string[] ObjectTypes = new string[]
		//{
		//	"class",
		//	"struct",
		//	"interface",

		//};

		private const string headerMarker = "#HEADER#";
		private const string referenceMarker = "#REFERENCE#";
		private const string nameSpaceMarker = "#NAMESPACE#";
		private const string accessibilityMarker = "#ACCESSIBILITY#";
		private const string objectTypeMarker = "#OBJECTTYPE#";
		private const string scriptNameMarker = "#SCRIPTNAME#";
		private const string statementMarker = "#STATEMENT#";


		public override void Action(int instanceId, string pathName, string resourceFile)
		{
			var source = File.ReadAllText(resourceFile);
			var fileName = Path.GetFileNameWithoutExtension(pathName);
			fileName = fileName.Replace(" ", "");

			// Replacements
			source = source.Replace(headerMarker, Header);
			source = source.Replace(referenceMarker, CreateReferences());
			source = source.Replace(nameSpaceMarker, NameSpace);
			source = source.Replace(accessibilityMarker, Accessibility);
			source = source.Replace(objectTypeMarker, ObjectType);
			source = source.Replace(scriptNameMarker, fileName);
			source = source.Replace(statementMarker, Statement);

			File.WriteAllText(pathName, source, new UTF8Encoding(true, false));
			AssetDatabase.ImportAsset(pathName);
			ProjectWindowUtil.ShowCreatedAsset(AssetDatabase.LoadAssetAtPath<MonoScript>(pathName));
		}

		private string CreateReferences()
		{
			var builder = new StringBuilder();

			foreach (var reference in References)
			{
				builder.Append("using ");
				builder.Append(reference);
				builder.Append(";\n");
			}

			return builder.ToString();
		}
	}
}