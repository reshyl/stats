using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Stat Definition")]
    public class StatDefinition : ScriptableObject
    {
        [Header("Meta Data")]
        public string id;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public Color color = Color.white;
        [Header("Runtime")]
        public float baseValue = 0f;
        public Formula formula;
    }
}
