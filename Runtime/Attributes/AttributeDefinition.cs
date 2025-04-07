using System;
using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Attribute Definition")]
    public class AttributeDefinition : ScriptableObject
    {
        [Header("Meta Data")]
        public string id;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public Color color = Color.white;
        [Header("Runtime")]
        public float minValue = 0f;
        public StatField maxValue;
        [Range(0f, 1f)] public float startPercent = 1f;
        public MaxValueChangeBehavior onMaxValueChanged;
    }

    public enum MaxValueChangeBehavior
    {
        [Tooltip("Keep the current value, but clamped to the new range.")]
        KeepValue,
        [Tooltip("Keep the current percent of the max value.")]
        KeepPercent,
        [Tooltip("Set the current value to the new max value.")]
        SetToMax,
    }
}
