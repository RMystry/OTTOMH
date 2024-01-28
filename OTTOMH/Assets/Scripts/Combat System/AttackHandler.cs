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

        private float damage;
        
        [SerializeField] private MeleeWeaponDescriptor meleeWeaponType;
        [SerializeField] private ArenaEffectDescriptor arenaWeaponType;
        [SerializeField] private RangedWeaponDescriptor rangedWeaponType;
        [SerializeField] private ThrownWeaponDescriptor thrownWeaponType;

        public float attackSpeed;
        public bool canAttack = true;
        public void CurrentWeaponWasChanged(WeaponDescriptor descriptor, AttackType attackType)
        {
            canAttack = true;
            currentWeaponAttackType = attackType;
            attackSpeed = descriptor.attackSpeed;
            damage = descriptor.damage;
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

        public void CommandAttack(Vector3 position)
        {
            if (!canAttack) 
            {
                return;
            }

            Collider[] collisions = new Collider[0];
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
                    hit = AttackWithThrownWeapon(position, out collisions);
                    break;
                case AttackType.ARENAEFFECT:
                    hit = AttackWithArenaWeapon(position, out collisions);
                    break;
            }

            if(hit)
            {
                OnSuccessfulHit(collisions);
            }

            canAttack = false;

            StartCoroutine(AttackTimer());
        }

        private IEnumerator AttackTimer()
        {
            yield return new WaitForSeconds(1 / attackSpeed);

            canAttack = true;
        }

        private bool AttackWithMeleeWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            // if we have a lunge, lunge forward

            var pos = transform.position + transform.forward * meleeWeaponType.range;

            return prefabHandler.UseWeapon(pos, meleeWeaponType, out collisions);
        }

        private bool AttackWithRangedWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            return  prefabHandler.UseWeapon(targetPosition, rangedWeaponType, out collisions);
        }

        private bool AttackWithThrownWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            return prefabHandler.UseWeapon(targetPosition, thrownWeaponType, out collisions);
        }

        private bool AttackWithArenaWeapon(Vector3 targetPosition, out Collider[] collisions)
        {
            throw new NotImplementedException();
        }


        private void OnSuccessfulHit(Collider[] colliders)
        {
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponentInParent<HealthHandler>() != null)
                    colliders[i].GetComponentInParent<HealthHandler>().TakeDamage(damage);

            }
        }

        internal void AttackInput(bool isPressed, Vector3 position)
        {
            Debug.Log(position);
            CommandAttack(position);
        }
    }
}
