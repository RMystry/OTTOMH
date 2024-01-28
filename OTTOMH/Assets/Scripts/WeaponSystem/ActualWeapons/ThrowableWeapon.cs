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
        public float throwHeight;
      
        public void UpdateStates()
        {

        }

        private void Reset()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override bool Attack(Vector3 position, ThrownWeaponDescriptor descriptor, out Collider[] collision)
        {
            GameObject projectile = Instantiate(objectToThrow, transform.position, transform.rotation);

            throwItem(projectile.transform, position, Vector3.Distance(position, projectile.transform.position), throwHeight);

            var collisionSphere = Physics.SphereCastAll(projectile.transform.position, descriptor.range, Vector3.zero);
            List<Collider> colliders = new List<Collider>();    

            foreach(var hit in collisionSphere)
            {
                if(hit.collider.GetComponentInParent<HealthHandler>() != null)
                {
                    colliders.Add(hit.collider);
                }
            }
            collision = colliders.ToArray();
            return true;
        }

        public AnimationCurve[] buildThrowTrajectory(Vector3 origin , Vector3 target, float throwLength, float throwHeight )
        {
            // Returns array of 3 AnimationCurves: 0/1/2 = X/Y/Z.

            // Initalise trajectory array.
            var trajectory= new AnimationCurve[3];
            trajectory[0] = new AnimationCurve();
            trajectory[1] = new AnimationCurve();
            trajectory[2] = new AnimationCurve();

            // Start point.
            trajectory[0].AddKey(0.0f, origin.x);
            trajectory[1].AddKey(0.0f, origin.y);
            trajectory[2].AddKey(0.0f, origin.z);

            // Mid point.
            trajectory[0].AddKey(throwLength * 0.5f, (origin.x + target.x) * 0.5f);
            trajectory[1].AddKey(throwLength * 0.5f, (origin.y + target.y) * 0.5f + throwHeight);
            trajectory[2].AddKey(throwLength * 0.5f, (origin.z + target.z) * 0.5f);

            // End point.
            trajectory[0].AddKey(throwLength, target.x);
            trajectory[1].AddKey(throwLength, target.y);
            trajectory[2].AddKey(throwLength, target.y);

            return trajectory;
        }

        public void  throwItem( Transform item,  Vector3 targetPoint, float throwLength, float throwHeight)
        {
            var itemInAir = true;
            float itemTravelTime = 0.0f;
            var trajectory = buildThrowTrajectory(item.position, targetPoint, throwLength, throwHeight);

            while (itemInAir)
            {
                item.position = new Vector3(trajectory[0].Evaluate(itemTravelTime), trajectory[1].Evaluate(itemTravelTime), trajectory[2].Evaluate(itemTravelTime));

                itemTravelTime += Time.deltaTime;
                if (itemTravelTime > throwLength)
                {
                    itemInAir = false;
                }

            }

            // Just to ensure that item is definitely at the target position after throw.
            item.position = targetPoint;
        }
    }
}
