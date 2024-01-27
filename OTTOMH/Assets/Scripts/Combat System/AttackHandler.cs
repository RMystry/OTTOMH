using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // determine what to attack with.
            switch(currentWeaponAttackType)
            {
                case AttackType.MELEE:
                    AttackWithMeleeWeapon();
                    break;
                case AttackType.RANGED:
                    AttackWithRangedWeapon();
                    break;
                case AttackType.THROWN:
                    AttackWithThrownWeapon();
                    break;
                case AttackType.ARENAEFFECT:
                    AttackWithArenaWeapon();
                    break;
            }
        }


        public void AttackWithMeleeWeapon()
        {
            // gets information from melee weapon.

            // we'll move a set distance if have lunge, and strike all enemies hit.
        }

        public void AttackWithRangedWeapon()
        {

        }

        public void AttackWithThrownWeapon()
        {

        }

        public void AttackWithArenaWeapon()
        {

        }
    }
}
