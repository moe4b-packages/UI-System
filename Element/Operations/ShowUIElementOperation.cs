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
    [AddComponentMenu(UI.Path + "Operations/" + "Show UI Element")]
    public class ShowUIElementOperation : Operation
	{
        [SerializeField]
        UIElement target = default;
        public UIElement Target => target;

        void Reset()
        {
            target = ComponentQuery.Single.In<UIElement>(this, ComponentQuery.Self, ComponentQuery.Parents);
        }

        public override void Execute()
        {
            target.Show();
        }
    }
}