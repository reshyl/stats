using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomPropertyDrawer(typeof(StatField))]
    public class StatFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (Application.isPlaying)
                GUI.enabled = false;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var typeProp = property.FindPropertyRelative("type");
            var statProp = property.FindPropertyRelative("definition");
            var statIdProp = property.FindPropertyRelative("statID");
            var valueProp = property.FindPropertyRelative("staticValue");

            Rect statRect = new Rect(position.x, position.y, position.width * 0.65f, position.height);
            Rect typeRect = new Rect(position.x + position.width * 0.68f, position.y, position.width * 0.32f, position.height);

            EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

            switch ((StatFieldType)typeProp.enumValueIndex)
            {
                case StatFieldType.Definition:
                    EditorGUI.PropertyField(statRect, statProp, GUIContent.none);
                    break;
                case StatFieldType.ID:
                    EditorGUI.PropertyField(statRect, statIdProp, GUIContent.none);
                    break;
                case StatFieldType.Float:
                    EditorGUI.PropertyField(statRect, valueProp, GUIContent.none);
                    break;
                case StatFieldType.Integer:
                    valueProp.floatValue = EditorGUI.IntField(statRect, Mathf.RoundToInt(valueProp.floatValue));
                    break;
            }

            GUI.enabled = true;
            EditorGUI.EndProperty();
        }
    }
}
