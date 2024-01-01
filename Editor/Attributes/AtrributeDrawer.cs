using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomPropertyDrawer(typeof(Attribute))]
    public class AtrributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var definitionProp = property.FindPropertyRelative("definition");

            EditorGUI.PropertyField(position, definitionProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
