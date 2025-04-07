using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reshyl.Stats
{
    /// <summary>
    /// The most simple formula for calculating the value of a Stat.
    /// Adds all flat modifiers first, then applies percent modifiers, and finally
    /// multiplies the final value with (the sum of) all multiplier modifiers.
    /// </summary>
    [CreateAssetMenu(menuName = "Stats/Default Formula", fileName = "Default Formula")]
    public class DefaultFormula : Formula
    {
        public bool roundResultToInt = false;
        [Tooltip("Assume percent modifiers are already divided by hundred.")]
        public bool percentIsNormalized = false;

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

            if (percentIsNormalized)
                finalValue *= 1f + percentSum;
            else
                finalValue *= 1f + (percentSum / 100f);
            
            finalValue *= 1f + multiplierSum;

            if (roundResultToInt)
                finalValue = Mathf.RoundToInt(finalValue);

            return finalValue;
        }
    }
}
