using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    public abstract class Formula : ScriptableObject
    {
        public abstract float CalculateValue(float baseValue, IEnumerable<Modifier> modifiers);
    }
}
