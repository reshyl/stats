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
        [SerializeField] private StatDefinition stat;
        [SerializeField] private string statID;
        [SerializeField] private float staticValue;

        /// <summary>
        /// Get the ID of the Stat assigned to this field. Returns "constant" if
        /// the type is an int/float.
        /// </summary>
        public string GetID(StatsContainer stats)
        {
            if (type == StatFieldType.Stat)
                return stat.id;
            else if (type == StatFieldType.StatID)
                return statID;
            else
                return "constant";
        }

        /// <summary>
        /// Get the display name of the Stat assigned to this field. Returns "Constant" if 
        /// the type is an int/float.
        /// </summary>
        public string GetDisplayName(StatsContainer stats)
        {
            if (type == StatFieldType.Stat)
                return stat.displayName;
            else if (type == StatFieldType.StatID)
            {
                if (stats.HasStat(statID, out var stat))
                    return stat.Definition.displayName;
                else
                    return statID;
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
            if (type == StatFieldType.Stat)
                return stats.GetStat(stat);
            else if (type == StatFieldType.StatID)
                return stats.GetStat(statID);
            else
                return null;
        }

        /// <summary>
        /// Get the current value of the Stat assigned to this field. Returns the constant value
        /// assigned in the inspector if the type is int/float.
        /// </summary>
        public float GetValue(StatsContainer stats)
        {
            var value = 0f;

            if (type == StatFieldType.Stat)
            {
                if (stats.HasStat(stat, out var statRef))
                    value = statRef.Value;
                else
                    Debug.LogError(stats.name + " does not contain " + stat.id);
            }
            else if (type == StatFieldType.StatID)
            {
                if (stats.HasStat(statID, out var statRef))
                    value = statRef.Value;
                else
                    Debug.LogError(stats.name + " does not contain " + statID);
            }
            else
            {
                value = staticValue;
            }

            return value;
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
