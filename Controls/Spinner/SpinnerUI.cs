using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEditor;
using UnityEditor.UI;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MB.UISystem
{
    [AddComponentMenu(UI.Paths.Controls + "Spinner")]
    public class SpinnerUI : Selectable, IMoveHandler
    {
        [SerializeField]
        Text label;
        public Text Label => label;

        [SerializeField]
        ButtonsProperty buttons;
        public ButtonsProperty Buttons => buttons;
        [Serializable]
        public class ButtonsProperty
        {
            [SerializeField]
            Button next;
            public Button Next => next;

            [SerializeField]
            Button previous;
            public Button Previous => previous;
        }

        [SerializeField]
        List<string> options;
        public IReadOnlyList<string> Options => options;

        public const string OptionFallback = "None";

        [SelectionField]
        [SerializeField]
        int internal_index;
        public int Index
        {
            get => internal_index;
            set
            {
                internal_index = value;
                UpdateState();
                onSubmit.Invoke(Index);
            }
        }

        [SerializeField]
        WrapAroundMode wrapAround = WrapAroundMode.Auto;
        public WrapAroundMode WrapAroud => wrapAround;
        public enum WrapAroundMode
        {
            Auto, On, Off
        }

        public bool WrapAroundEvaluation
        {
            get
            {
                switch (wrapAround)
                {
                    case WrapAroundMode.Auto:
                        return options.Count <= 2;

                    case WrapAroundMode.On:
                        return true;

                    case WrapAroundMode.Off:
                        return false;
                }

                throw new NotImplementedException();
            }
        }

        [Space]
        [SerializeField]
        UnityEvent<int> onSubmit;
        public UnityEvent<int> OnSubmit => onSubmit;

        protected override void OnValidate()
        {
            base.OnValidate();

            UpdateState();
        }

        protected override void Start()
        {
            base.Start();

            buttons.Next.onClick.AddListener(Next);
            buttons.Previous.onClick.AddListener(Previous);
        }

        public void Add(string entry)
        {
            options.Add(entry);

            UpdateState();
        }
        public void Add(IEnumerable<string> entries)
        {
            options.AddRange(entries);

            UpdateState();
        }

        public void Clear()
        {
            options.Clear();
            Index = 0;
        }

        void UpdateState()
        {
            Label.text = options.SafeIndexer(Index, OptionFallback);

            if(options.Count == 0)
            {
                buttons.Next.interactable = false;
                buttons.Previous.interactable = false;
            }
            else
            {
                if (WrapAroundEvaluation)
                {
                    buttons.Next.interactable = true;
                    buttons.Previous.interactable = true;
                }
                else
                {
                    buttons.Next.interactable = Options.Count > 0 && Index < Options.Count - 1;
                    buttons.Previous.interactable = Options.Count > 0 && Index > 0;
                }
            }
        }

        void Next()
        {
            if (Index + 1 >= Options.Count)
            {
                if (WrapAroundEvaluation == false)
                    return;

                Index = 0;
            }
            else
            {
                Index += 1;
            }
        }
        void Previous()
        {
            if (Index - 1 < 0)
            {
                if (WrapAroundEvaluation == false)
                    return;

                Index = options.Count - 1;
            }
            else
            {
                Index -= 1;
            }
        }

        public override Selectable FindSelectableOnLeft()
        {
            Previous();
            return default;
        }
        public override Selectable FindSelectableOnRight()
        {
            Next();
            return default;
        }

        [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
        public class SelectionFieldAttribute : PropertyAttribute
        {
#if UNITY_EDITOR
            [CustomPropertyDrawer(typeof(SelectionFieldAttribute))]
            public class Drawer : PropertyDrawer
            {
                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                {
                    return EditorGUIUtility.singleLineHeight;
                }

                public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
                {
                    if (property.IsEditingMultipleObjects())
                    {
                        EditorGUI.HelpBox(rect, "Cannot Modify Selection for Multiple Components", MessageType.Info);
                        return;
                    }

                    var selector = property.serializedObject.targetObject as SpinnerUI;

                    EditorGUI.BeginProperty(rect, label, property);

                    //Prefix Label
                    {
                        var content = new GUIContent("Selection");
                        rect = EditorGUI.PrefixLabel(rect, content);
                    }

                    //Dropdown Button
                    {
                        var text = selector.options.SafeIndexer(property.intValue, OptionFallback);
                        var content = new GUIContent(text);

                        if (EditorGUI.DropdownButton(rect, content, FocusType.Keyboard))
                        {
                            var menu = new GenericMenu();

                            for (int i = 0; i < selector.options.Count; i++)
                                menu.AddItem(selector.options[i], i == property.intValue, Select, i);

                            menu.ShowAsContext();

                            void Select(object data)
                            {
                                var value = (int)data;
                                property.LateModifyProperty(x => x.intValue = value);
                            }
                        }
                    }

                    EditorGUI.EndProperty();
                }
            }
#endif
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(SpinnerUI))]
        public class Inspector : SelectableEditor
        {
            public string[] Names = new string[]
            {
                "label",
                "buttons",
                "options",
                "internal_index",
                "wrapAround",
                "onSubmit",
            };

            public SerializedProperty[] Properties;

            protected override void OnEnable()
            {
                base.OnEnable();

                Properties = Array.ConvertAll(Names, (x) => serializedObject.FindProperty(x));
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                EditorGUILayout.Space();

                for (int i = 0; i < Properties.Length; i++)
                    EditorGUILayout.PropertyField(Properties[i], true);

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}