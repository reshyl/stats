using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    /// <summary>
    /// Base class that can be overriden to define a custom formula for calculating the value
    /// of a Stat after all modifiers are applied.
    /// </summary>
    public abstract class Formula : ScriptableObject
    {
        public abstract float CalculateValue(float baseValue, IEnumerable<Modifier> modifiers);
    }
}
