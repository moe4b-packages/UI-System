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
	[AddComponentMenu(Paths.Variants + "Popup Panel")]
	public class PopupPanel : UIPanel
	{
		UIData UI;
		[Serializable]
		public class UIData
        {
			[SerializeField]
			internal Text label;

			[SerializeField]
			internal Button button;

			[SerializeField]
			internal Text instruction;
		}

		Action callback;

		public override void Configure()
		{
			base.Configure();

			UI.button.onClick.AddListener(Click);
		}

		public MRoutine.Handle Show(string text) => Show(text, null, null);
		public MRoutine.Handle Show(string text, string instruction) => Show(text, instruction, null);
		public MRoutine.Handle Show(string text, string instruction, Action callback)
		{
			UI.label.text = text;
			UI.instruction.text = instruction;
			UI.button.gameObject.SetActive(instruction != null);

			this.callback = callback;

			return Show();
		}

		void Click()
		{
			Hide();

			Invoke();
		}

		void Invoke()
        {
			var cache = callback;
			callback = null;
			cache?.Invoke();
		}
	}
}