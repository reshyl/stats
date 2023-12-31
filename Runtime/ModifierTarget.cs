using System;

namespace Reshyl.Stats
{
    /// <summary>
    /// A utility class to help select the target Stat for a Modifier in the inspector.
    /// </summary>
    [Serializable]
    public class ModifierTarget
    {
        public bool useId;
        public StatDefinition stat;
        public string statId;
    }
}
