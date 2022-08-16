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
	[AddComponentMenu(Paths.Variants + "Text Input Panel")]
	public class TextInputPanel : UIPanel
	{
		UIData UI;
		[Serializable]
		public class UIData
		{
			[SerializeField]
			internal Text label;

			[SerializeField]
			internal InputField input;

			[SerializeField]
			internal Button okay;

			[SerializeField]
			internal Button cancel;
		}

		public InputField.ContentType ContentType
		{
			get => UI.input.contentType;
			set => UI.input.contentType = value;
		}

		public delegate void CallbackDelegate(bool confirmed, string text);
		public CallbackDelegate callback;

		public override void Configure()
		{
			base.Configure();

			UI.okay.onClick.AddListener(Confirm);
			UI.cancel.onClick.AddListener(Deny);

            OnTransition += TransitionCallback;
		}

        void TransitionCallback(bool isOn)
        {
            if(isOn)
            {
				ContentType = InputField.ContentType.Standard;
			}
			else
            {
				UI.input.text = string.Empty;
			}
		}

        public virtual MRoutine.Handle Show(string instructions, CallbackDelegate callback)
		{
			UI.label.text = instructions;
			this.callback = callback;

			SetSelection(UI.input);

			return Show();
		}

		void Confirm() => Action(true);
		void Deny() => Action(false);

		void Action(bool confirmed)
		{
			var text = UI.input.text;

			Hide();

			Invoke(confirmed, text);
		}

		void Invoke(bool confirmed, string text)
		{
			var cache = callback;
			callback = null;
			cache?.Invoke(confirmed, text);
		}
	}
}