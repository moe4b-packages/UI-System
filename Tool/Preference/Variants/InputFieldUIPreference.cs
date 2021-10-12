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
    [AddComponentMenu(Path + "Input Field Preference")]
    public class InputFieldUIPreference : UIPreference<InputField, string>
    {
        public override string Data
        {
            get => Component.text;
            set => Component.text = value;
        }

        protected override void RegisterCallback() => Component.onValueChanged.AddListener(ChangeCallback);
    }
}