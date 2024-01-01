using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomPropertyDrawer(typeof(AttributeField))]
    public class AttributeFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (Application.isPlaying)
                GUI.enabled = false;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var typeProp = property.FindPropertyRelative("type");
            var defProp = property.FindPropertyRelative("definition");
            var attIdProp = property.FindPropertyRelative("attributeID");

            Rect attRect = new Rect(position.x, position.y, position.width * 0.65f, position.height);
            Rect typeRect = new Rect(position.x + position.width * 0.68f, position.y, position.width * 0.32f, position.height);

            EditorGUI.PropertyField(typeRect, typeProp, GUIContent.none);

            switch ((AttributeFieldType)typeProp.enumValueIndex)
            {
                case AttributeFieldType.Definition:
                    EditorGUI.PropertyField(attRect, defProp, GUIContent.none);
                    break;
                case AttributeFieldType.ID:
                    EditorGUI.PropertyField(attRect, attIdProp, GUIContent.none);
                    break;
            }

            GUI.enabled = true;
            EditorGUI.EndProperty();
        }
    }
}
