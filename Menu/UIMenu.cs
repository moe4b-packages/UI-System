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
    [AddComponentMenu(UI.Path + "UI Menu")]
    public class UIMenu : UIElement
    {
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