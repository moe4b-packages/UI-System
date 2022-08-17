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

using UnityEngine.PlayerLoop;
using System.Text;

namespace MB.UISystem
{
    public abstract class UIPreference : MonoBehaviour
    {
        public const string Path = UI.Paths.Tools + "Preference/";
        
        [SerializeField]
        protected string ID;

        protected virtual void Reset()
        {
            ID = MUtility.Unity.GetHierarchyPath(transform);
        }
    }

    public abstract class UIPreference<TComponent, TData> : UIPreference, PreAwake.IInterface
        where TComponent : Component
    {
        [SerializeField]
        TData _default = default;
        public TData Default => _default;

        [ReadOnly]
        [SerializeField]
        protected TComponent component;
        public TComponent Component => component;

        public abstract TData Data { get; set; }

        public virtual void PreAwake()
        {
            component = GetComponent<TComponent>();
        }

        void Awake()
        {
            ManualLateStart.Register(LateStart);
        }

        void LateStart()
        {
            gameObject.SetActive(false); //Used to hide UI transitions

            Data = Load();

            RegisterCallback();

            gameObject.SetActive(true);
        }

        protected virtual void Save(TData data) => AutoPreferences.Set(ID, data);
        protected virtual TData Load() => AutoPreferences.Read<TData>(ID);

        protected abstract void RegisterCallback();

        protected virtual void ChangeCallback(TData data) => Save(data);
    }
}