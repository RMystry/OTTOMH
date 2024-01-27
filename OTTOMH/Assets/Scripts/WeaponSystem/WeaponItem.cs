using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class WeaponItem : Interactable
    {
        public WeaponDescriptor containedWeapon;


        public override void Interact(GameObject interactionSource)
        {
            base.Interact(interactionSource);

            
            interactionSource.GetComponent<WeaponHandler>().PickedUpWeapon(containedWeapon);
            Destroy(gameObject);
        }
    }
}
