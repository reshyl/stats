using Reshyl.Stats;
using UnityEditor;
using UnityEngine;

namespace ReshylEditor.Stats
{
    [CustomEditor(typeof(Traits))]
    public class TraitsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var runtimeStats = ((Traits)target).RuntimeStats;
            var origStats = ((Traits)target).originalStats;

            if (Application.isPlaying)
            {

            }
            else
            {
                
            }
        }
    }
}
