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
		[SerializeField]
		Text label = default;

		[SerializeField]
		InputField input = default;

		public InputField.ContentType ContentType
        {
			get => input.contentType;
			set => input.contentType = value;
        }

		[SerializeField]
		Button okay = default;

		[SerializeField]
		Button cancel = default;

		public delegate void CallbackDelegate(bool confirmed, string text);
		public CallbackDelegate callback;

		public override void Configure()
		{
			base.Configure();

			okay.onClick.AddListener(Confirm);
			cancel.onClick.AddListener(Deny);

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
				input.text = string.Empty;
			}
		}

        public virtual MRoutine.Handle Show(string instructions, CallbackDelegate callback)
		{
			label.text = instructions;
			this.callback = callback;

			SetSelection(input);

			return Show();
		}

		void Confirm() => Action(true);
		void Deny() => Action(false);

		void Action(bool confirmed)
		{
			var text = input.text;

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