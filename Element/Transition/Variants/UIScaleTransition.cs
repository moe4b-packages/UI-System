using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using System;

namespace MB.UISystem
{
    [AddComponentMenu(Path + "Scale Transition")]
    public class UIScaleTransition : UITransition
	{
        [SerializeField]
        Transform target;

        [SerializeField]
        ValueRange range = new ValueRange(0, 1f);

        [SerializeField]
        AxisMode axis;
        [Serializable]
        public class AxisMode
        {
            [SerializeField]
            bool x;
            public bool X => x;

            [SerializeField]
            bool y;
            public bool Y => y;

            [SerializeField]
            bool z;
            public bool Z => z;

            public AxisMode()
            {
                x = true;
                y = true;
                z = true;
            }
        }

        void Reset()
        {
            target = transform;
        }

        public override void Apply(bool isOn, float rate)
        {
            var scale = target.localScale;

            var value = range.Lerp(rate);

            if (axis.X) scale.x = value;
            if (axis.Y) scale.y = value;
            if (axis.Z) scale.z = value;

            target.localScale = scale;
        }
    }
}