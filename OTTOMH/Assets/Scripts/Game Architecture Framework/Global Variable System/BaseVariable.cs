using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [System.Serializable]
    public abstract class BaseVariable<T> : ScriptableObject
    {
        public T Value;
    }
}
