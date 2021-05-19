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
    [AddComponentMenu(UI.Path + "Operations/" + "Hide UI Element")]
    public class HideUIElementOperation : Operation
	{
		[SerializeField]
        UIElement target = default;
        public UIElement Target => target;

        void Reset()
        {
            target = QueryComponent.InParents<UIElement>(this);
        }

        public override void Execute()
        {
            target.Hide();
        }
    }
}