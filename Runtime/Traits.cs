using System.Collections.Generic;
using UnityEngine;

namespace Reshyl.Stats
{
    public class Traits : MonoBehaviour
    {
        public Modifier test;
        [SerializeField]
        protected Class originalClass;
        public Class Class { get; protected set; }

        protected virtual void Start()
        {
            Class = originalClass.GetRuntimeClass();
        }
    }
}
