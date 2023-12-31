using UnityEngine;

namespace Reshyl.Stats
{
    public class Traits : MonoBehaviour
    {
        public StatsContainer originalStats;

        public StatsContainer RuntimeStats { get; protected set; }

        protected virtual void Start()
        {
            RuntimeStats = originalStats.GetRuntimeCopy();
        }
    }
}
