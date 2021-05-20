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
    [AddComponentMenu(UI.Path + "UI Panel")]
    public class UIPanel : UIElement
    {
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