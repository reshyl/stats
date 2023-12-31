using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] protected StatDefinition definition;
        [SerializeField] protected bool overrideBaseValue = false;
        [SerializeField] protected float newBaseValue = 0f;

        protected float cachedValue;
        protected bool isDirty = true;
        protected List<Modifier> modifiers = new List<Modifier>();

        /// <summary>
        /// The base value of the Stat, without any modifiers. Takes into consideration if it
        /// has been overwritten.
        /// </summary>
        public virtual float BaseValue => GetBaseValue();
        /// <summary>
        /// The current value of the Stat, using applied modifiers.
        /// </summary>
        
        public virtual float Value => GetValue();
        public StatDefinition Definition => definition;
        public string ID => Definition.id;
        
        /// <summary>
        /// The formula being used to calculate the value. Can be used to calculate the value manually.
        /// </summary>
        public Formula Formula => Definition.formula;

        protected virtual float GetBaseValue()
        {
            var baseValue = overrideBaseValue ? newBaseValue : Definition.baseValue;
            return baseValue;
        }

        protected virtual float GetValue()
        {
            if (isDirty)
            {
                cachedValue = Formula.CalculateValue(BaseValue, modifiers);
                isDirty = false;
            }

            return cachedValue;
        }

        /// <summary>
        /// Get the modifiers currently effecting this Stat
        /// </summary>
        public IEnumerable<Modifier> GetCurrentModifiers()
        {
            return modifiers;
        }

        /// <summary>
        /// Simulate the value of this Stat using a custom list of Modifiers.
        /// </summary>
        /// <param name="includeCurrent">If true, simulates with the Modifiers currently added to the Stat as well.</param>
        public virtual float GetSimulatedValue(IEnumerable<Modifier> modifiers, bool includeCurrent)
        {
            var simulatedModifiers = new List<Modifier>();

            if (includeCurrent)
                simulatedModifiers.AddRange(this.modifiers);

            simulatedModifiers.AddRange(modifiers);

            return Formula.CalculateValue(BaseValue, simulatedModifiers);
        }

        /// <returns>Whether modifier was added successfully. Only false if the modifer provided
        /// targets a different Stat.</returns>
        public virtual bool AddModifier(Modifier modifier)
        {
            modifiers ??= new List<Modifier>();

            if (modifier.target.useId)
            {
                if (modifier.target.statId != Definition.id)
                    return false;
            }
            else
            {
                if (modifier.target.stat != Definition)
                    return false;
            }

            modifiers.Add(modifier);
            isDirty = true;
            return true;
        }

        /// <summary>
        /// Removes the given modifier instance from this Stat
        /// </summary>
        /// <returns>Whether modifier was removed successfully.</returns>
        public virtual bool RemoveModifier(Modifier modifier)
        {
            if (modifiers == null || modifiers.Count <= 0)
                return false;

            if (modifiers.Contains(modifier))
            {
                modifiers.Remove(modifier);
                isDirty = true;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Remove all modifiers on this Stat with the given ID.
        /// </summary>
        /// <param name="id">The modifier's ID.</param>
        /// <returns>If any modifiers were removed at all.</returns>
        public virtual bool RemoveModifiersWithID(string id)
        {
            if (modifiers == null || modifiers.Count <= 0)
                return false;

            var removed = false;

            for (int i = modifiers.Count - 1; i >= 0; i--)
            {
                if (modifiers[i].id == id)
                {
                    RemoveModifier(modifiers[i]);
                    removed = true;
                }
            }

            return removed;
        }

        /// <returns>Returns false if there were no modifiers to begin with.</returns>
        public virtual bool RemoveAllModifiers()
        {
            if (modifiers == null || modifiers.Count <= 0)
                return false;

            for (int i = modifiers.Count - 1; i >= 0; i--)
                RemoveModifier(modifiers[i]);

            return true;
        }
    }
}
