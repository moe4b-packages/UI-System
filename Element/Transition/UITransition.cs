using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

using System;

namespace MB.UISystem
{
	public abstract class UITransition : MonoBehaviour
	{
		public const string Path = UIElement.Paths.Root + "Transition/";

		public abstract void Apply(bool isOn, float rate);
	}
}