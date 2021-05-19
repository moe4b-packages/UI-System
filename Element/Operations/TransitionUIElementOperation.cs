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
    [AddComponentMenu(UI.Path + "Operations/" + "Transition UI Element")]
    public class TransitionUIElementOperation : Operation
	{
        [SerializeField]
        UIElement current = default;
        public UIElement Current => current;

        [SerializeField]
        UIElement next = default;
        public UIElement Next => next;

        void Reset()
        {
            current = QueryComponent.In<UIElement>(this, QueryComponent.Self, QueryComponent.Parents);
        }

        public override void Execute()
        {
            current.Hide();
            next.Show();
        }
    }
}