﻿using UnityEditor;
using UnityEngine;

namespace Assets.Skripte.Scrips_Marcel
{
	public class sandbox_testscript : ScriptableObject
	{
		[MenuItem("Tools/MyTool/Do It in C#")]
		static void DoIt()
		{
			EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
		}
	}
}