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

using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MB.UISystem
{
    [AddComponentMenu(Paths.Root + "UI Element")]
    public class UIElement : MonoBehaviour, IInitialize, PreAwake.IInterface
    {
        public static class Paths
        {
            public const string Root = UI.Paths.Root + "Element/";

            public const string Operations = Root + "Operations/";
        }

        [field: SerializeField]
        public bool IsOn { get; private set; }

        [SerializeField]
        protected TransitionProperty transition;
        [Serializable]
        public class TransitionProperty
        {
            [SerializeField]
            internal float duration = 0.2f;

            [SerializeField]
            internal bool activate = true;

            [SerializeField]
            [HideInInspector]
            internal List<UITransition> elements = new List<UITransition>();

            public bool IsAssigned => elements.Count > 0;

            internal void Apply(bool isOn, float rate)
            {
                for (int i = 0; i < elements.Count; i++)
                    elements[i].Apply(isOn, rate);
            }
        }

        public float Rate { get; private set; }

        public virtual void PreAwake()
        {
            ComponentQuery.Collection.InHierarchy(this, transition.elements);
        }

        protected virtual void OnValidate()
        {
            ComponentQuery.Collection.InHierarchy(this, transition.elements);

            SetState(IsOn);
        }

        public virtual void Configure()
        {

        }
        public virtual void Initialize()
        {

        }

        public MRoutine.Handle Show() => Translate(true);
        public MRoutine.Handle Hide() => Translate(false);

        public MRoutine.Handle Toggle()
        {
            if (IsOn)
                return Hide();
            else
                return Show();
        }

        public delegate void TransitionDelegate(bool isOn);
        public event TransitionDelegate OnTransition;

        MRoutine.Handle routine;
        public MRoutine.Handle Translate(bool value)
        {
            if (routine.IsProcessing)
                routine.Stop();

            IsOn = value;
            OnTransition?.Invoke(IsOn);

            routine = MRoutine.Create(Procedure).Start();
            IEnumerator Procedure()
            {
                if (transition.activate && IsOn == true)
                    gameObject.SetActive(true);

                if (transition.IsAssigned)
                {
                    var target = IsOn ? 1f : 0f;

                    while (true)
                    {
                        Rate = Mathf.MoveTowards(Rate, target, Time.deltaTime / transition.duration);

                        transition.Apply(IsOn, Rate);

                        yield return MRoutine.Wait.Frame();

                        if (Mathf.Approximately(Rate, target))
                            break;
                    }
                }

                if (transition.activate && IsOn == false)
                    gameObject.SetActive(false);
            }
            return routine;
        }

        public void SetState(bool value)
        {
            IsOn = value;
            Rate = IsOn ? 1f : 0f;

            transition.Apply(IsOn, Rate);

            if (transition.activate) gameObject.SetActive(IsOn);
        }

        public UIElement()
        {
            transition = new TransitionProperty();
        }

        //Static Utility

        public EventSystem EventSystem => EventSystem.current;

        public GameObject Selection
        {
            get => EventSystem.currentSelectedGameObject;
            set => EventSystem.SetSelectedGameObject(value);
        }

        public void SetSelection(UObjectSurrogate surrogate) => Selection = surrogate.GameObject;
    }
}