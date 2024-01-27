using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Architecture/Global Variables/Interactable", fileName = "New Interactable Global Variable")]
    public class InteractableVariable : BaseVariable<Interactable> 
    { 
        public void SetValue(Interactable val)
        {
            Value = val;
        }
    }
}
