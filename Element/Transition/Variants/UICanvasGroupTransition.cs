using UnityEngine;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

namespace MB.UISystem
{
    [AddComponentMenu(Path + "Canvas Group Transition")]
    public class UICanvasGroupTransition : UITransition
    {
        [SerializeField]
        CanvasGroup group;

        [SerializeField]
        bool reverse;

        void Reset()
        {
            group = GetComponent<CanvasGroup>();
        }

        public override void Apply(bool isOn, float rate)
        {
            if (reverse)
                rate = 1 - rate;

            group.alpha = rate;
            group.blocksRaycasts = !isOn ^ reverse;
        }
    }
}