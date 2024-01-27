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


        [SerializeField] private MeleeWeaponDescriptor meleeWeaponType;
        [SerializeField] private ArenaEffectDescriptor arenaWeaponType;
        [SerializeField] private RangedWeaponDescriptor rangedWeaponType;
        [SerializeField] private ThrownWeaponDescriptor thrownWeaponType;

        public WeaponBase currentWeapon;

        public void CurrentWeaponWasChanged(WeaponDescriptor descriptor, AttackType attackType)
        {
            currentWeaponAttackType = attackType;
            switch(attackType)
            {
                case AttackType.MELEE:
                    meleeWeaponType = descriptor as MeleeWeaponDescriptor;
                    arenaWeaponType = null;
                    rangedWeaponType = null;
                    thrownWeaponType = null;
                    break;
                case AttackType.ARENAEFFECT:
                    meleeWeaponType = null;
                    arenaWeaponType = descriptor as ArenaEffectDescriptor;
                    rangedWeaponType = null;
                    thrownWeaponType = null;
                    break;
                case AttackType.THROWN:
                    meleeWeaponType = null;
                    arenaWeaponType = null;
                    rangedWeaponType = null;
                    thrownWeaponType = descriptor as ThrownWeaponDescriptor;
                    break;
                case AttackType.RANGED:
                    meleeWeaponType = null;
                    arenaWeaponType = null;
                    rangedWeaponType = descriptor as RangedWeaponDescriptor;
                    thrownWeaponType = null;
                    break;
                default:
                    currentWeaponAttackType = AttackType.MELEE;
                    meleeWeaponType = descriptor as MeleeWeaponDescriptor;
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


        public void OnSuccessfulHit(Collider collider)
        {

        }
    }
}
