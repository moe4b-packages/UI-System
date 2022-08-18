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
    [AddComponentMenu(Paths.Root + "UI Menu")]
    public class UIMenu : UIElement
    {
        public new static class Paths
        {
            public const string Root = UI.Paths.Root + "Menu/";

            public const string Variants = Root + "Variants/";
        }
        
        protected virtual void OnEnable()
        {
            Active = this;
        }
        protected virtual void OnDisable()
        {
            if (Active == this) Active = null;
        }

        public static UIMenu Active { get; protected set; }
    }
}