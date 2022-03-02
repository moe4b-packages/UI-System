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
    [AddComponentMenu(Path + "Dropdown Preference")]
    public class DropdownUIPreference : UIPreference<Dropdown, int>
    {
        public override int Data
        {
            get => component.value;
            set => component.value = value;
        }

        protected override void RegisterCallback() => component.onValueChanged.AddListener(ChangeCallback);
    }
}