using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Architecture/Global Variables/Game Object", fileName = "New GameObject Global Variable")]
    public class GameObjectVariable : BaseVariable<GameObject> 
    {
        public void SetValue(GameObject value)
        {
            Value = value;
        }
    }
}
