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
    [AddComponentMenu(Path + "Toggle Preference")]
    public class ToggleUIPreference : UIPreference<Toggle, bool>
    {
        public override bool Data
        {
            get => component.isOn;
            set => component.isOn = value;
        }

        protected override void RegisterCallback() => component.onValueChanged.AddListener(ChangeCallback);
    }
}