using System;
using UnityEngine;

namespace Reshyl.Stats
{
    [Serializable]
    public class AttributeField
    {
        [SerializeField] private AttributeFieldType type;
        [SerializeField] private AttributeDefinition definition;
        [SerializeField] private string attributeID;

        private Attribute cachedAttribute;

        /// <summary>
        /// Get the current Value of the attribute assigned to this field.
        /// </summary>
        public float GetCurrentValue(StatsContainer stats)
        {
            return GetAttribute(stats).Value;
        }

        /// <summary>
        /// Get the minimum Value of the attribute assigned to this field.
        /// </summary>
        public float GetMinValue(StatsContainer stats)
        {
            return GetAttribute(stats).MinValue;
        }

        /// <summary>
        /// Get the maximum Value of the attribute assigned to this field.
        /// </summary>
        public float GetMaxValue(StatsContainer stats)
        {
            return GetAttribute(stats).MaxValue;
        }

        /// <summary>
        /// Get the runtime instance of the attribute assigned to this field. Will be null
        /// if the given StatsContainer does not manage the attribute.
        /// </summary>
        public Attribute GetAttribute(StatsContainer stats)
        {
            if (cachedAttribute == null)
            {
                if (type == AttributeFieldType.Definition)
                    cachedAttribute = stats.GetAttribute(definition);
                else if (type == AttributeFieldType.ID)
                    cachedAttribute = stats.GetAttribute(attributeID);
            }

            return cachedAttribute;
        }
    }
}
