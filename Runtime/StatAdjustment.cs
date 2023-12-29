using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Stat Adjustment")]
    public class StatAdjustment : ScriptableObject
    {
        [Serializable]
        public class AdjustmentModifier
        {
            public ModifierTarget target;
            public float value;
            public ModifierType type;
        }

        public string id;
        public int order;
        public List<AdjustmentModifier> modifiers;

        /// <summary>
        /// Convert the adjustments into modifiers usable by the Stat class.
        /// </summary>
        public IEnumerable<Modifier> GetModifiers()
        {
            var mods = new List<Modifier>();

            for (int i = 0; i < modifiers.Count; i++)
            {
                var adjustment = modifiers[i];

                var modifier = new Modifier
                {
                    id = id,
                    target = adjustment.target,
                    value = adjustment.value,
                    type = adjustment.type,
                    order = order
                };

                mods.Add(modifier);
            }

            return mods;
        }
    }
}
