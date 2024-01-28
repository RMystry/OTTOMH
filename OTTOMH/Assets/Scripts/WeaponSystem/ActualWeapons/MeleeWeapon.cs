using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GGJ
{
    public class MeleeWeapon : WeaponBase<MeleeWeaponDescriptor>
    {
        Vector3 castPosition;
        Vector3 size;
        public override bool Attack(Vector3 position, MeleeWeaponDescriptor descriptor, out Collider[] collision)
        {
            // this is really just directions. 
            // spherecast in an area ahead of the position.

            if(particles != null)
            {
                particles.Play();
            }


            var listOfColliders = new List<Collider>();
            castPosition = position;
            

            size = new Vector3(descriptor.range, 2, descriptor.range);
            var hits = Physics.BoxCastAll(position, size, GameManager.Player.transform.forward);
            var result = false;
            foreach(var hit in hits)
            {
                Debug.Log("Hit: " + hit.collider.name);
                if(hit.collider.GetComponentInParent<HealthHandler>() != null)
                {
                    listOfColliders.Add(hit.collider);
                    result = true;
                }

            }
            collision = listOfColliders.ToArray();

            return result;
        }



        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawCube(castPosition, size);
        }
    }
}
