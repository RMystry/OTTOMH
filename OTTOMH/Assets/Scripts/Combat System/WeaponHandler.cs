using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    [AddComponentMenu("Combat System/Weapon Handler")]
    public class WeaponHandler : MonoBehaviour
    {
        public WeaponDescriptor pickedUpWeapon1;
        public WeaponDescriptor pickedUpWeapon2;


        public WeaponsList combinedWeaponsList;

        public WeaponDescriptor currentWeapon;

        public UnityEvent<WeaponDescriptor, AttackType> OnCurrentWeaponChanged;
        public UnityEvent<GameObject> OnPrefabSwitch;

        public void PickedUpWeapon(WeaponDescriptor weapon)
        {
            // first we're going to check if either weapon is currently held.

            // so now we have
            if (pickedUpWeapon1 == null)
            {
                pickedUpWeapon1 = weapon;

                currentWeapon = pickedUpWeapon1;
            }
            else if (pickedUpWeapon2 == null)
            {
                pickedUpWeapon2 = weapon;

                // check combination.
                currentWeapon = CheckWeaponComboList();
            }
            else if (pickedUpWeapon1 != null && pickedUpWeapon2 != null)
            {
                // both are filled so will remove the current weapon.
                pickedUpWeapon1 = weapon;
                currentWeapon = pickedUpWeapon1;
            }

            WeaponChanged();
        }


        private WeaponDescriptor CheckWeaponComboList()
        {
            for(int i = 0; i < combinedWeaponsList.Length; i++)
            {
                if (combinedWeaponsList[i].components.CheckIfComboMet(pickedUpWeapon1, pickedUpWeapon2))
                {
                    return combinedWeaponsList[i];
                }
            }

            return pickedUpWeapon2;
        }
        private void WeaponChanged()
        {
            // tries to cast the current weapon to another
            var attackType = AttackType.MELEE;

            if(currentWeapon is MeleeWeaponDescriptor)
            {
                attackType = AttackType.MELEE;
            }
            else if(currentWeapon is RangedWeaponDescriptor)
            {
                attackType = AttackType.RANGED;
            }
            else if(currentWeapon is ThrownWeaponDescriptor)
            {
                attackType = AttackType.THROWN;
            }
            else if(currentWeapon is ArenaEffectDescriptor)
            {
                attackType = AttackType.ARENAEFFECT;
            }

            OnCurrentWeaponChanged?.Invoke(currentWeapon, attackType);
            OnPrefabSwitch?.Invoke(currentWeapon.weaponPrefab);
        }

    }
}
