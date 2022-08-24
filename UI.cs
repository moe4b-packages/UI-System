using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[assembly: AssemblySymbolDefine("MB_UI")]

namespace MB.UISystem
{
	public static class UI
	{
		public static class Paths
		{
			public const string Root = Toolbox.Paths.Root + "UI System/";

			public const string Tools = Root + "Tools/";

			public const string Controls = Root + "Controls/";
		}
	}
}