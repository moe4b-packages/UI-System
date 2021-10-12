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
    [AddComponentMenu(Paths.Root + "UI Panel")]
    public class UIPanel : UIElement
    {
        public new static class Paths
        {
            public const string Root = UI.Paths.Root + "Panel/";

            public const string Variants = Root + "Variants/";
        }
        
        protected virtual void OnEnable() => Register(this);

        protected virtual void OnDisable() => Unregister(this);

        //Static Utility

        public static List<UIPanel> List { get; protected set; }

        static void Register(UIPanel item) => List.Add(item);
        static bool Unregister(UIPanel item) => List.Remove(item);

        static UIPanel()
        {
            List = new List<UIPanel>();
        }
    }
}