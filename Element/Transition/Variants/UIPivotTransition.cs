using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

using System;

namespace MB.UISystem
{
    [AddComponentMenu(Path + "Pivot Transition")]
    public class UIPivotTransition : UITransition
    {
        [SerializeField]
        RectTransform rect;

        [SerializeField]
        AxisMode axis = AxisMode.X;
        public enum AxisMode
        {
            X, Y
        }

        public float Pivot
        {
            get
            {
                switch (axis)
                {
                    case AxisMode.X:
                        return rect.pivot.x;

                    case AxisMode.Y:
                        return rect.pivot.y;

                    default:
                        throw new NotImplementedException();
                }
            }
            set
            {
                var pivot = rect.pivot;

                switch (axis)
                {
                    case AxisMode.X:
                        pivot.x = value;
                        break;

                    case AxisMode.Y:
                        pivot.y = value;
                        break;

                    default:
                        throw new NotImplementedException();
                }

                rect.pivot = pivot;
            }
        }

        [SerializeField]
        RangeData range;
        [Serializable]
        public struct RangeData
        {
            [SerializeField]
            float on;
            public float On => on;

            [SerializeField]
            float off;
            public float Off => off;

            public float Lerp(float rate) => Mathf.Lerp(off, on, rate);
        }

        void Reset()
        {
            rect = GetComponent<RectTransform>();
        }

        public override void Apply(bool isOn, float rate)
        {
            var target = range.Lerp(rate);

            Pivot = target;
        }
    }

}