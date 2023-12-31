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

        public int GetValueInt(StatsContainer stats)
        {
            return Mathf.RoundToInt(GetValue(stats));
        }
    }
}
