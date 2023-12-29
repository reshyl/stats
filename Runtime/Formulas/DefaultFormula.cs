using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Default Formula")]
    public class DefaultFormula : Formula
    {
        public override float CalculateValue(float baseValue, IEnumerable<Modifier> modifiers)
        {
            var flatModifiers = modifiers.Where(m => m.type == ModifierType.Flat);
            var percentModifiers = modifiers.Where(m => m.type == ModifierType.Percent);
            var multipliers = modifiers.Where(m => m.type == ModifierType.Multiplier);

            var flatSum = flatModifiers.Sum(m => m.value);
            var percentSum = percentModifiers.Sum(m => m.value);
            var multiplierSum = multipliers.Sum(m => m.value);

            var finalValue = baseValue;
            finalValue += flatSum;
            finalValue *= 1f + (percentSum / 100f);
            finalValue *= 1f + multiplierSum;

            return finalValue;
        }
    }
}
