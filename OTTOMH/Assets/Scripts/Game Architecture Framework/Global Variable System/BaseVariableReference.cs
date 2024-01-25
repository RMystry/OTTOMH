using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGJ
{
    [System.Serializable]
    public class BaseVariableReference<T1, T2> where T1 : BaseVariable<T2>
    {
        public bool UseConstant = true;
        public T2 ConstantValue;
        public T1 Variable;

        public T2 Value
        {
            get {
                return UseConstant ? ConstantValue :
                                       Variable.Value;
            }
        }
    }

    [System.Serializable]
    public class FloatReference : BaseVariableReference<FloatVariable, float> 
    { }
    
    [System.Serializable]
    public class StringReference : BaseVariableReference<StringVariable, string>
    { }

    [System.Serializable]
    public class IntReference : BaseVariableReference<IntVariable, int>
    { }

    [System.Serializable]
    public class BoolReference : BaseVariableReference<BoolVariable, bool>
    { }

    [System.Serializable]
    public class GameObjectReference : BaseVariableReference<GameObjectVariable, GameObject>
    { }


}
