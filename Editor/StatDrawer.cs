using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomPropertyDrawer(typeof(Stat))]
    public class StatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var definitionProp = property.FindPropertyRelative("definition");
            var overrideBoolProp = property.FindPropertyRelative("overrideBaseValue");
            var newBaseValueProp = property.FindPropertyRelative("newBaseValue");

            var definition = (StatDefinition)definitionProp.objectReferenceValue;

            if (definition != null)
            {
                var defRect = new Rect(position.x, position.y, position.width * 0.65f, position.height);
                var valueRect = new Rect(position.x + position.width * 0.68f, position.y,
                    position.width * 0.19f, position.height);
                var overrideRect = new Rect(position.x + position.width * 0.9f, position.y,
                    position.width * 0.1f, position.height);

                EditorGUI.PropertyField(defRect, definitionProp, GUIContent.none);
                EditorGUI.PropertyField(overrideRect, overrideBoolProp, GUIContent.none);

                if (overrideBoolProp.boolValue)
                {
                    EditorGUI.PropertyField(valueRect, newBaseValueProp, GUIContent.none);
                }
                else
                {
                    GUI.enabled = false;
                    newBaseValueProp.floatValue = definition.baseValue;
                    EditorGUI.PropertyField(valueRect, newBaseValueProp, GUIContent.none);
                    GUI.enabled = true;
                }
            }
            else
            {
                EditorGUI.PropertyField(position, definitionProp, GUIContent.none);
            }

            EditorGUI.EndProperty();
        }
    }
}
