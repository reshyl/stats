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
    }
}
