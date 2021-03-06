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
    [AddComponentMenu(UIElement.Paths.Operations + "Transition UI Element Operation")]
    public class TransitionUIElementOperation : Operation.Process
	{
        [SerializeField]
        UIElement current = default;
        public UIElement Current => current;

        [SerializeField]
        UIElement next = default;
        public UIElement Next => next;

        protected override void Reset()
        {
            base.Reset();

            current = ComponentQuery.Single.In<UIElement>(this, ComponentQuery.Self, ComponentQuery.Parents);
        }

        public override object Execute()
        {
            current.Hide();
            next.Show();
            return default;
        }
    }
}