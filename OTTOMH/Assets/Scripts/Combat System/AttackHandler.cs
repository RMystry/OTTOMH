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

        public WeaponPrefabHandler prefabHandler;
        
        [SerializeField] private MeleeWeaponDescriptor meleeWeaponType;
        [SerializeField] private ArenaEffectDescriptor arenaWeaponType;
        [SerializeField] private RangedWeaponDescriptor rangedWeaponType;
        [SerializeField] private ThrownWeaponDescriptor thrownWeaponType;

        

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
            Collider[] collisions = null;
            bool hit = false;
            // determine what to attack with.
            switch(currentWeaponAttackType)
            {
                case AttackType.MELEE:
                    hit = AttackWithMeleeWeapon(transform.forward, out collisions);
                    break;
                case AttackType.RANGED:
                    hit = AttackWithRangedWeapon(transform.forward,out collisions);
                    break;
                case AttackType.THROWN:
                    hit = AttackWithThrownWeapon(out collisions);
                    break;
                case AttackType.ARENAEFFECT:
                    hit = AttackWithArenaWeapon(out collisions);
                    break;
            }

            if(hit)
            {
                OnSuccessfulHit(collisions);
            }
        }


        private bool AttackWithMeleeWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            throw new NotImplementedException();
            //return prefabHandler.UseWeapon<MeleeWeapon, MeleeWeaponDescriptor>(transform.position, meleeWeaponType, out collisions);
        }

        private bool AttackWithRangedWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            return  prefabHandler.UseWeapon<RangedWeapon, RangedWeaponDescriptor>(transform.forward, rangedWeaponType, out collisions);
        }

        private bool AttackWithThrownWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            return prefabHandler.UseWeapon<ThrowableWeapon, ThrownWeaponDescriptor>(transform.position, thrownWeaponType, out collisions);
        }

        private bool AttackWithArenaWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            throw new NotImplementedException();
        }


        private void OnSuccessfulHit(Collider[] colliders)
        {
            
        }
    }
}
