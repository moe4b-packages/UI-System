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
    [AddComponentMenu(UI.Path + "UI Element")]
    public class UIElement : MonoBehaviour, IInitialize
    {
        public virtual GameObject Context => gameObject;

        public bool Visibile
        {
            get => Context.activeInHierarchy;
            set
            {
                if (value)
                    Show();
                else
                    Hide();
            }
        }

        public virtual void Configure()
        {

        }

        public virtual void Init()
        {

        }

        public event Action OnShow;
        public virtual void Show()
        {
            Context.SetActive(true);

            OnShow?.Invoke();
        }

        public event Action OnHide;
        public virtual void Hide()
        {
            Context.SetActive(false);

            OnHide?.Invoke();
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