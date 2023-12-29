using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Stat Definition")]
    public class StatDefinition : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea] public string description;
        public float baseValue = 0f;
        public Formula formula;
    }
}
