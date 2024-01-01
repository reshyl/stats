using System;

namespace Reshyl.Stats
{
    [Serializable]
    public class Modifier
    {
        public string id;
        public ModifierTarget target;
        public float value;
        public ModifierType type;
        public int order;
    }
}
