using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    [CreateAssetMenu(menuName = "Stats/Stats Container")]
    public class StatsContainer : ScriptableObject
    {
        [SerializeField] protected List<Attribute> attributes;
        [SerializeField] protected List<Stat> stats;

        public StatsContainer GetRuntimeCopy()
        {
            var copy = ScriptableObject.Instantiate(this);
            copy.stats.ForEach(s => s.Setup());
            copy.attributes.ForEach(a => a.Setup(copy));

            return copy;
        }

        public void Dispose()
        {
            for (int i = attributes.Count - 1; i >= 0; i--)
                attributes[i].Dispose();
        }

        //-------- Utility functions to get Stats --------

        public bool HasStat(string id)
        {
            return GetStat(id) != null;
        }

        public bool HasStat(string id, out Stat stat)
        {
            stat = GetStat(id);
            return stat != null;
        }

        public bool HasStat(StatDefinition definition)
        {
            return GetStat(definition) != null;
        }

        public bool HasStat(StatDefinition definition, out Stat stat)
        {
            stat = GetStat(definition);
            return stat != null;
        }

        public Stat GetStat(string id)
        {
            return stats.Find(s => s.ID == id);
        }

        public Stat GetStat(StatDefinition definition)
        {
            return stats.Find(s => s.Definition == definition);
        }

        //-------- Shortcuts to managing modifiers on Stats --------

        /// <summary>
        /// Find the Stat this modifier targets and add it to said Stat.
        /// </summary>
        /// <returns>False if the targeted Stat doesn't exist.</returns>
        public virtual bool AddModifier(Modifier modifier)
        {
            Stat stat;

            if (modifier.target.useId)
                stat = GetStat(modifier.target.statId);
            else
                stat = GetStat(modifier.target.stat);

            if (stat == null)
                return false;

            return stat.AddModifier(modifier);
        }

        /// <summary>
        /// Get a collection of modifiers from the given adjustment, and add them to their respective Stats.
        /// </summary>
        /// <returns>False if none of the targeted Stats exist.</returns>
        public virtual bool AddAdjustment(StatAdjustment adjustment)
        {
            var modifiers = adjustment.GetModifiers();
            return AddModifiers(modifiers);
        }

        /// <summary>
        /// Add a collection of modifiers to their respective targeted Stats.
        /// </summary>
        /// <returns>False if none of the modifiers were added. Only possible if they all target Stats that
        /// don't exist.</returns>
        public virtual bool AddModifiers(IEnumerable<Modifier> modifiers)
        {
            var added = false;

            foreach (var modifier in modifiers)
            {
                if (AddModifier(modifier))
                    added = true;
            }

            return added;
        }

        /// <summary>
        /// Find the Stat this modifier targets and remove it from said Stat.
        /// </summary>
        /// <returns>False if the targeted Stat doesn't exist.</returns>
        public virtual bool RemoveModifier(Modifier modifier)
        {
            Stat stat;

            if (modifier.target.useId)
                stat = GetStat(modifier.target.statId);
            else
                stat = GetStat(modifier.target.stat);

            if (stat == null)
                return false;

            return stat.RemoveModifier(modifier);
        }

        /// <summary>
        /// Go through all Stats and remove any modifiers with the given ID.
        /// </summary>
        /// <param name="id">The ID of the modifier to look for.</param>
        /// <returns>False if no valid modifiers were found on any Stat.</returns>
        public virtual bool RemoveModifiersWithID(string id)
        {
            var removed = false;

            for (int i = stats.Count - 1; i >= 0; i--)
            {
                if (stats[i].RemoveModifiersWithID(id))
                    removed = true;
            }

            return removed;
        }

        /// <summary>
        /// Remove all modifiers with the same ID as the provided adjustment.
        /// </summary>
        /// <returns>False if there weren't any modifiers with the adjustment's ID.</returns>
        public virtual bool RemoveAdjustment(StatAdjustment adjustment)
        {
            return RemoveModifiersWithID(adjustment.id);
        }

        /// <summary>
        /// Find the provided Stat and remove all modifiers from it.
        /// </summary>
        /// <returns>False if the Stat doesn't exist, or it has no modifiers.</returns>
        public virtual bool RemoveAllModifiersFrom(StatDefinition definition)
        {
            var stat = GetStat(definition);

            if (stat == null)
                return false;

            return stat.RemoveAllModifiers();
        }

        /// <summary>
        /// Find the Stat with the given ID and remove all modifiers from it.
        /// </summary>
        /// <returns>False if the Stat doesn't exist, or it has no modifiers.</returns>
        public virtual bool RemoveAllModifiersFrom(string statID)
        {
            var stat = GetStat(statID);

            if (stat == null)
                return false;

            return stat.RemoveAllModifiers();
        }

        //-------- Utility functions to get Attributes --------

        public bool HasAttribute(string id)
        {
            return GetAttribute(id) != null;
        }

        public bool HasAttribute(string id, out Attribute attribute)
        {
            attribute = GetAttribute(id);
            return attribute != null;
        }

        public bool HasAttribute(AttributeDefinition definition)
        {
            return GetAttribute(definition) != null;
        }

        public bool HasAttribute(AttributeDefinition definition, out Attribute attribute)
        {
            attribute = GetAttribute(definition);
            return attribute != null;
        }

        public Attribute GetAttribute(string id)
        {
            return attributes.Find(a => a.ID == id);
        }

        public Attribute GetAttribute(AttributeDefinition definition)
        {
            return attributes.Find(a => a.Definition == definition);
        }
    }
}
