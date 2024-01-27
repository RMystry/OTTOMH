using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    [AddComponentMenu("Combat System/Attack Handler")]
    [RequireComponent(typeof(WeaponHandler))]
    public class AttackHandler : MonoBehaviour
    {
        // basically we will have a weapon descriptor that gives us all the information for what weapon stats we will be using.
        public AttackType currentWeaponAttackType;

        public FloatReference attackModifier;


        [SerializeField] private MeleeWeapon meleeWeaponType;
        [SerializeField] private ArenaEffect arenaWeaponType;
        [SerializeField] private RangedWeapon rangedWeaponType;
        [SerializeField] private ThrownWeapon thrownWeaponType;

        public void CurrentWeaponWasChanged(WeaponDescriptor descriptor, AttackType attackType)
        {
            currentWeaponAttackType = attackType;
            switch(attackType)
            {
                case AttackType.MELEE:
                    meleeWeaponType = descriptor as MeleeWeapon;
                    arenaWeaponType = null;
                    rangedWeaponType = null;
                    thrownWeaponType = null;
                    break;
                case AttackType.ARENAEFFECT:
                    meleeWeaponType = null;
                    arenaWeaponType = descriptor as ArenaEffect;
                    rangedWeaponType = null;
                    thrownWeaponType = null;
                    break;
                case AttackType.THROWN:
                    meleeWeaponType = null;
                    arenaWeaponType = null;
                    rangedWeaponType = null;
                    thrownWeaponType = descriptor as ThrownWeapon;
                    break;
                case AttackType.RANGED:
                    meleeWeaponType = null;
                    arenaWeaponType = null;
                    rangedWeaponType = descriptor as RangedWeapon;
                    thrownWeaponType = null;
                    break;
                default:
                    currentWeaponAttackType = AttackType.MELEE;
                    meleeWeaponType = descriptor as MeleeWeapon;
                    arenaWeaponType = null;
                    rangedWeaponType = null;
                    thrownWeaponType = null;
                    break;
            }

        }

        public void CommandAttack()
        {

        }
    }

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
            return pickedUpWeapon1;
        }
        private void WeaponChanged()
        {
            // tries to cast the current weapon to another
            var attackType = AttackType.MELEE;

            if(currentWeapon is MeleeWeapon)
            {
                attackType = AttackType.MELEE;
            }
            else if(currentWeapon is RangedWeapon)
            {
                attackType = AttackType.RANGED;
            }
            else if(currentWeapon is ThrownWeapon)
            {
                attackType = AttackType.THROWN;
            }
            else if(currentWeapon is ArenaEffect)
            {
                attackType = AttackType.ARENAEFFECT;
            }

            OnCurrentWeaponChanged?.Invoke(currentWeapon, attackType);
        }

    }
        public enum AttackType
        {
            MELEE,
            THROWN,
            RANGED,
            ARENAEFFECT,
        }
}
