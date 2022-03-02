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
    [AddComponentMenu(UIElement.Paths.Operations + "Hide UI Element Operation")]
    public class HideUIElementOperation : Operation.Process
	{
		[SerializeField]
        UIElement target = default;
        public UIElement Target => target;

        protected override void Reset()
        {
            base.Reset();

            target = ComponentQuery.Single.In<UIElement>(this, ComponentQuery.Self, ComponentQuery.Parents);
        }

        public override object Execute()
        {
            target.Hide();
            return null;
        }
    }
}