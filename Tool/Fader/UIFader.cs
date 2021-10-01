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
		DurationData duration;
		public DurationData Duration => duration;
		[Serializable]
		public class DurationData
        {
			[SerializeField]
			float on = 1f;
			public float On => on;

			[SerializeField]
			float off = 1f;
			public float Off => off;
		}

		[SerializeField]
		bool isOn;
		public bool IsOn => isOn;

		public Image Image { get; protected set; }

		public CanvasGroup CanvasGroup { get; protected set; }

		public Color Color
		{
			get => Image.color;
			set => Image.color = value;
		}

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

		void OnValidate()
        {
			Image = GetComponent<Image>();
			CanvasGroup = GetComponent<CanvasGroup>();

			SetState(isOn);
		}

		public void Configure()
		{
			Image = GetComponent<Image>();
			CanvasGroup = GetComponent<CanvasGroup>();
		}

		public void Initialize()
		{
			
		}

		/// <summary>
		/// Instantly set state without transition
		/// </summary>
		/// <param name="value"></param>
		public void SetState(bool value)
		{
			isOn = value;

			Alpha = isOn ? 1f : 0f;
			BlockRaycasts = isOn;
		}

		public Coroutine Show(float? duration = null)
		{
			isOn = true;

			if (duration == null) duration = this.duration.On;

			return Process(duration.Value);
		}

		public Coroutine Toggle(float? duration = null)
        {
			if (IsOn)
				return Hide(duration: duration);
			else
				return Show(duration: duration);
        }

		public Coroutine Hide(float? duration = null)
		{
			isOn = false;

			if (duration == null) duration = this.duration.Off;

			return Process(duration.Value);
		}

		Coroutine Process(float duration)
        {
			if (coroutine != null) StopCoroutine(coroutine);

			coroutine = StartCoroutine(Procedure(duration));

			return coroutine;
        }

		public event Action OnBegin;
		void Begin()
        {
			OnBegin?.Invoke();
        }

		Coroutine coroutine;
		public IEnumerator Procedure(float duration)
		{
			Begin();

			BlockRaycasts = IsOn;

			var timer = 0f;

			while (true)
			{
				timer = Mathf.MoveTowards(timer, duration, Time.unscaledDeltaTime);

				Alpha = isOn ? Mathf.Lerp(0, 1, timer / duration) : Mathf.Lerp(1, 0, timer / duration);

				if (Mathf.Approximately(timer, duration)) break;

				yield return new WaitForEndOfFrame();
			}

			End();
		}

		public event Action OnEnd;
		void End()
        {
			coroutine = null;

			OnEnd?.Invoke();
        }
    }
}