using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomPropertyDrawer(typeof(ModifierTarget))]
    public class ModifierTargetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var useIdProp = property.FindPropertyRelative("useId");
            var statProp = property.FindPropertyRelative("stat");
            var statIdProp = property.FindPropertyRelative("statId");

            var statRect = new Rect(position.x, position.y, position.width * 0.65f, position.height);
            var popupRect = new Rect(position.x + position.width * 0.68f, position.y, 
                position.width * 0.32f, position.height);

            var options = new string[] { "Stat", "ID" };
            var selectedIndex = useIdProp.boolValue ? 1 : 0;

            selectedIndex = EditorGUI.Popup(popupRect, selectedIndex, options);
            useIdProp.boolValue = selectedIndex == 1;

            if (useIdProp.boolValue)
                EditorGUI.PropertyField(statRect, statIdProp, GUIContent.none);
            else
                EditorGUI.PropertyField(statRect, statProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}
