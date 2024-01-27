using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [System.Serializable]
    public class WeaponComponents
    {
        public WeaponDescriptor weapon1;
        public WeaponDescriptor weapon2;


        public bool CheckIfComboMet(WeaponDescriptor weapon1, WeaponDescriptor weapon2)
        {
            if (this.weapon1.weaponClass == weapon1.weaponClass && this.weapon2.weaponClass == weapon2.weaponClass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
