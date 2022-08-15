using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

namespace MB.UISystem
{
	public class UIScaleTransition : UITransition
	{
        [SerializeField]
        Transform target;

        [SerializeField]
        ValueRange range = new ValueRange(0, 1f);

        void Reset()
        {
            target = transform;
        }

        public override void Apply(bool isOn, float rate)
        {
            target.localScale = Vector3.one * range.Lerp(rate);
        }
    }
}