using System;
using UnityEngine;

namespace Reshyl.Stats
{
    /// <summary>
    /// A utility class to easily reference a Stat in the inspector.
    /// Supports referencing stats by StatDefinition or ID, or defining a simple float/int
    /// for testing purposes.
    /// </summary>
    [Serializable]
    public class StatField
    {
        [SerializeField] private StatFieldType type;
        [SerializeField] private StatDefinition definition;
        [SerializeField] private string statID;
        [SerializeField] private float staticValue;

        private Stat cachedStat;

        /// <summary>
        /// Get the ID of the Stat assigned to this field. Returns "constant" if
        /// the type is an int/float.
        /// </summary>
        public string GetID(StatsContainer stats)
        {
            if (type == StatFieldType.Definition)
                return definition == null ? string.Empty : definition.id;
            else if (type == StatFieldType.ID)
                return statID;
            else
                return "constant";
        }

        /// <summary>
        /// Get the display name of the Stat assigned to this field. Returns "Constant" if 
        /// the type is an int/float, or an empty string if the Stat couldn't be found.
        /// </summary>
        public string GetDisplayName(StatsContainer stats)
        {
            if (type == StatFieldType.Definition)
                return definition == null ? string.Empty : definition.displayName;
            else if (type == StatFieldType.ID)
            {
                if (GetStat(stats) != null)
                    return cachedStat.Definition.displayName;
                else
                    return string.Empty;
            }
            else
                return "Constant";
        }

        /// <summary>
        /// Get the Stat assigned to this field. Will be null if the given StatContainer does not have
        /// the Stat, or if the field is an int/float.
        /// </summary>
        public Stat GetStat(StatsContainer stats)
        {
            if (type == StatFieldType.Float || type == StatFieldType.Integer)
                return null;

            if (cachedStat == null)
            {
                if (type == StatFieldType.Definition)
                    cachedStat = stats.GetStat(definition);
                else if (type == StatFieldType.ID)
                    cachedStat = stats.GetStat(statID);
                else
                    cachedStat = null;
            }

            return cachedStat;
        }

        /// <summary>
        /// Get the current value of the Stat assigned to this field. Returns the constant value
        /// assigned in the inspector if the type is int/float.
        /// </summary>
        public float GetValue(StatsContainer stats)
        {
            if (type == StatFieldType.Float || type == StatFieldType.Integer)
                return staticValue;

            return GetStat(stats).Value;
        }

        /// <summary>
        /// Same as GetValue() but the result is rounded to int.
        /// </summary>
        public int GetValueInt(StatsContainer stats)
        {
            return Mathf.RoundToInt(GetValue(stats));
        }
    }
}
