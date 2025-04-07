using System;
using UnityEngine;

namespace Reshyl.Stats
{
    [Serializable]
    public class Attribute
    {
        [SerializeField] protected AttributeDefinition definition;

        public AttributeDefinition Definition => definition;
        public string ID => Definition.id;

        protected StatsContainer stats;
        protected float currentValue;
        protected bool maxValueIsStat;
        protected Stat maxValueStat;

        public float MinValue => Definition.minValue;
        public float MaxValue => GetMaxValue();
        public float Value => Mathf.Clamp(currentValue, MinValue, MaxValue);

        protected virtual float GetMaxValue()
        {
            if (maxValueIsStat)
                return maxValueStat.Value;
            else
                return Definition.maxValue.GetValue(stats);
        }

        /// <summary>
        /// Setup this Attribute to make sure it has references to the required Stats.
        /// </summary>
        public virtual void Setup(StatsContainer stats)
        {
            this.stats = stats;
            maxValueStat = Definition.maxValue.GetStat(stats);
            maxValueIsStat = maxValueStat != null;

            currentValue = MaxValue * Definition.startPercent;

            if (maxValueIsStat)
                maxValueStat.OnStatUpdated += OnMaxValueStatUpdated;
        }

        public virtual void Dispose()
        {
            if (maxValueIsStat)
                maxValueStat.OnStatUpdated -= OnMaxValueStatUpdated;

            stats = null;
            maxValueStat = null;
        }

        /// <summary>
        /// Change the current Value by the given amount.
        /// </summary>
        /// <returns>The current Value after the change.</returns>
        public virtual float ChangeValue(float amount)
        {
            currentValue += amount;
            currentValue = Mathf.Clamp(currentValue, MinValue, MaxValue);
            return currentValue;
        }

        protected virtual void OnMaxValueStatUpdated(float oldValue)
        {
            if (definition.onMaxValueChanged == MaxValueChangeBehavior.KeepValue)
            {
                currentValue = Mathf.Clamp(currentValue, MinValue, MaxValue);
            }
            else if (definition.onMaxValueChanged == MaxValueChangeBehavior.KeepPercent)
            {
                var percent = currentValue / oldValue;
                currentValue = Mathf.Clamp(percent * MaxValue, MinValue, MaxValue);
            }
            else if (definition.onMaxValueChanged == MaxValueChangeBehavior.SetToMax)
            {
                currentValue = MaxValue;
            }
        }
    }
}
