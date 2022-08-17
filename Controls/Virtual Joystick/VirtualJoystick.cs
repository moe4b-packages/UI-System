using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace MB.UISystem
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    [AddComponentMenu(UI.Paths.Controls + "Virtual Joystick")]
    public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField]
        RectTransform socket;
        public RectTransform Socket => socket;

        [SerializeField]
        RectTransform handle;
        public RectTransform Handle => handle;

        [SerializeField]
        bool recenter = true;
        public bool Recenter => recenter;
        
        [field: ReadOnly]
        [field: SerializeField]
        public Vector2 Value { get; private set; }

        public Vector2 RestPosition { get; private set; }

        public RectTransform transform { get; private set; }
        public Camera camera { get; private set; }
        public Canvas Canvas { get; private set; }

        void Awake()
        {
            transform = base.transform as RectTransform;
            camera = Camera.main;
            Canvas = ComponentQuery.Single.InParents<Canvas>(this);
        }
        void Start()
        {
            RestPosition = socket.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData data)
        {
            var point = ScreenPointToRectPosition(data.position);

            if (recenter)
            {
                socket.position = point;
                handle.localPosition = Vector3.zero;
            }
            else
            {
                socket.anchoredPosition = RestPosition;
                handle.position = point;
            }

            ProcessInput();
        }
        public void OnDrag(PointerEventData data)
        {
            var point = ScreenPointToRectPosition(data.position);

            handle.position = point;

            ProcessInput();
        }
        public void OnPointerUp(PointerEventData data)
        {
            socket.anchoredPosition = RestPosition;
            handle.anchoredPosition = Vector2.zero;

            Value = Vector2.zero;
        }

        void ProcessInput()
        {
            var range = socket.sizeDelta.x / 2f;

            var vector = handle.anchoredPosition;
            vector = Vector3.ClampMagnitude(vector, range);

            handle.anchoredPosition = vector;
            Value = vector / range;
        }

        public Vector3 ScreenPointToRectPosition(Vector2 point)
        {
            return RectTransformUtility.PixelAdjustPoint(point, transform, Canvas);
        }
    }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
}