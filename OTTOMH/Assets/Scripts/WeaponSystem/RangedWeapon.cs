﻿using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class RangedWeapon : WeaponBase<RangedWeaponDescriptor>
    {
        public override bool Attack(Vector3 position, RangedWeaponDescriptor descriptor, out Collider[] collision)
        {
            var shootPosition = GameManager.Player.transform.position + new Vector3(0f, 0.75f, 0f);
            bool hits = false; 
            // okay so I need to calculate a forward rotation to set the direction to based on the ranged weapon's 
            List<Collider> collisions = new List<Collider>();
            for(int i = 0; i < descriptor.shotCount; i++)
            {
                Vector3 direction = transform.forward + Random.insideUnitSphere * descriptor.spread;

                if(Physics.Raycast(shootPosition, direction, out var hit, descriptor.range))
                {
                    if(hit.collider.GetComponentInParent<HealthHandler>() != null || hit.collider.GetComponent<HealthHandler>())
                        collisions.Add(hit.collider);


                    hits = true;
                }    

            }

            collision = collisions.ToArray();

            return hits;
        }
    }
}