using System;

namespace Reshyl.Stats
{
    [Serializable]
    public class Modifier
    {
        public string id;
        public StatDefinition targetStat;
        public bool useIdInstead;
        public string targetStatId;
        public float value;
        public ModifierType type;
        public int order;
    }
}
