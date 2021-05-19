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
	[RequireComponent(typeof(Image))]
	[RequireComponent(typeof(CanvasGroup))]
	public class UIFader : MonoBehaviour, IInitialize
	{
		[SerializeField]
        float duration = 2f;
        public float Duration => duration;

		public bool IsOn { get; protected set; }

		public Image Image { get; protected set; }

		public Color Color
        {
			get => Image.color;
			set => Image.color = value;
        }

		public CanvasGroup CanvasGroup { get; protected set; }

		public float Alpha
        {
			get => CanvasGroup.alpha;
			set => CanvasGroup.alpha = value;
        }

		public bool BlockRaycasts
        {
			get => CanvasGroup.blocksRaycasts;
			set => CanvasGroup.blocksRaycasts = value;
        }

		public void Configure()
		{
			Image = GetComponent<Image>();
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		public void Init()
		{
			
		}

		public void Show()
		{
			IsOn = true;

			Process();
		}

		public void Toggle()
        {
			if (IsOn)
				Hide();
			else
				Show();
        }

		public void Hide()
		{
			IsOn = false;

			Process();
		}

		void Update()
        {
			if (Input.GetKeyDown(KeyCode.Mouse0))
				Toggle();
		}

		void Process()
        {
			if (coroutine != null) StopCoroutine(coroutine);

			coroutine = StartCoroutine(Procedure());
        }

		Coroutine coroutine;
		public IEnumerator Procedure()
		{
			BlockRaycasts = IsOn;

			var timer = 0f;

			while (true)
			{
				timer = Mathf.MoveTowards(timer, duration, Time.unscaledDeltaTime);

				Alpha = IsOn ? Mathf.Lerp(0, 1, timer / duration) : Mathf.Lerp(1, 0, timer / duration);

				if (Mathf.Approximately(timer, duration)) break;

				yield return new WaitForEndOfFrame();
			}

			coroutine = null;
		}
    }
}