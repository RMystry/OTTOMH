using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class ThrowableWeapon : WeaponBase
    {
        [Header("References")]
        public Transform cam;
        public GameObject objectToThrow;
        public Transform attackPoint;

        [Header("Throwing")]
        public float throwForce;
        public float throwUpwardForce;

       

        public bool readyToThrow;
        public void UpdateStates()
        {

        }

        public override void Attack()
        {

            GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.transform.rotation);

            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            Vector3 forceToAdd = cam.transform.forward * throwForce + cam.transform.up * throwUpwardForce;
            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);


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
    }
}
