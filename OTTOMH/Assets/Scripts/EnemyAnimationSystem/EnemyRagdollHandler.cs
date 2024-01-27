using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class EnemyRagdollHandler : MonoBehaviour
    {
        public EnemyRigidbodyController RagdollController;
        public GameObject Enemy;
        public Chmicken enemyMovement;
        public HealthHandler healthHandler;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void RagdollThisThing()
        {
            enemyMovement.stopMovement = true;


            GetComponent<Rigidbody>().AddForce(-transform.forward * 35f, ForceMode.Impulse);
        }

        public void startRagdoll(ForceInfo force)
        {
            enemyMovement.stopMovement = true;
            RagdollController.TemporaryRagdoll(force);
            
        }

        public void OnRagdollCompleted()
        {
            enemyMovement.stopMovement= false;
        }

        public void OnDeath(ForceInfo force)
        {
            RagdollController.Kill(force);
        }
    }
}
