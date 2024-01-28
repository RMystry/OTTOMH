using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace GGJ
{
    public class Projectile : MonoBehaviour
    {
        public bool done { get; private set; }

        private float range;

        public ParticleSystem explosionParticleSystem;

        public GlobalEvent explosionEvent;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Throw(Vector3 position, float throwHeight, ThrownWeaponDescriptor descriptor)
        {
            range = descriptor.range;
            ThrowLogic(position, throwHeight, descriptor);
        }

        private void ThrowLogic(Vector3 position, float throwHeight, ThrownWeaponDescriptor descriptor)
        {
            StartCoroutine(throwItem(transform, position, Vector3.Distance(position, transform.position), throwHeight, descriptor));
        }

        public AnimationCurve[] buildThrowTrajectory(Vector3 origin, Vector3 target, float throwLength, float throwHeight)
        {
            // Returns array of 3 AnimationCurves: 0/1/2 = X/Y/Z.

            // Initalise trajectory array.
            var trajectory = new AnimationCurve[3];
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
            trajectory[2].AddKey(throwLength, target.z);

            return trajectory;
        }

        public IEnumerator throwItem(Transform item, Vector3 targetPoint, float throwLength, float throwHeight, ThrownWeaponDescriptor descriptor)
        {
            var itemInAir = true;
            float itemTravelTime = 0.0f;
            var trajectory = buildThrowTrajectory(item.position, targetPoint, throwLength, throwHeight);

            while (itemInAir)
            {
                item.position = new Vector3(trajectory[0].Evaluate(itemTravelTime), trajectory[1].Evaluate(itemTravelTime), trajectory[2].Evaluate(itemTravelTime));

                itemTravelTime += Time.deltaTime * 15f;
                if (itemTravelTime > throwLength)
                {
                    itemInAir = false;
                }
                yield return new WaitForFixedUpdate();
            }

            // Just to ensure that item is definitely at the target position after throw.
            item.position = targetPoint;




            var collisionSphere = Physics.SphereCastAll(transform.position, descriptor.range, transform.up);
            List<Collider> colliders = new List<Collider>();

            foreach (var hit in collisionSphere)
            {
                var handler = hit.collider.GetComponentInParent<HealthHandler>();
                if (handler != null)
                {
                    Debug.Log("Hit a parent for " + descriptor.damage + " trauma damage!");
                    handler.TakeDamage(descriptor.damage);
                }
            }

            done = true;

            if(explosionParticleSystem != null)
            {
                explosionParticleSystem.gameObject.SetActive(true);
                explosionParticleSystem.Play();
                Debug.Log("Waiting Some Time: " + explosionParticleSystem.main.duration * 5f);
                yield return new WaitForSeconds(explosionParticleSystem.main.duration * 5f);
            }

            if(explosionEvent != null)
                explosionEvent.Raise();

            Debug.Log("Destroying now!");
            Destroy(gameObject);
        }

        
        private void OnDrawGizmos()
        {
            if (!done) return;

            Gizmos.color = Color.blue;

            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
