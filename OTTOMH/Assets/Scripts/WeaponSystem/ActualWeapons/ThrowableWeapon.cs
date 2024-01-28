using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace GGJ
{
    public class ThrowableWeapon : WeaponBase<ThrownWeaponDescriptor>
    {
        [Header("References")]
        public GameObject objectToThrow;

        [Header("Throwing")]
        public float throwHeight = 10.0f;
      
  

        public override bool Attack(Vector3 position, ThrownWeaponDescriptor descriptor, out Collider[] collision)
        {
            OnAttackUnityEvent?.Invoke();

            objectToThrow = descriptor.projectile;
            GameObject projectile = Instantiate(objectToThrow, transform.position, Quaternion.identity);

            projectile.GetComponent<Projectile>().Throw(position, throwHeight, descriptor);


            collision = new Collider[0];

            return true;
        }

    }
}
