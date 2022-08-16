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

namespace MB.UISystem
{
	[AddComponentMenu(UI.Paths.Tools + "Fade UI")]
	public class FadeUI : UIElement
	{
		public MRoutine.Handle Show(float duration)
		{
			transition.duration = duration;

			return Show();
		}

		public MRoutine.Handle Hide(float duration)
		{
			transition.duration = duration;

			return Hide();
		}
	}
}