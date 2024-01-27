using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [RequireComponent(typeof(HealthHandler), typeof(AttackHandler), typeof(Chmicken))]
    public class EnemyCharacter : MonoBehaviour
    {
        public HealthHandler HealthHandler;
        public AttackHandler AttackHandler;
        public Chmicken MovementHandler;
        public int pollingRate = 10;
        public GameObject nearestObject;
        public int polling = 0;
        // Start is called before the first frame update
        void Start()
        {
            HealthHandler = GetComponent<HealthHandler>();
            AttackHandler = GetComponent<AttackHandler>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ApplyEffects()
        {

        }


        private void LateUpdate()
        {
            if (polling == pollingRate)
            {
                nearestObject = FindNearestObject();
                polling = 0;
            }
            else
            {
                polling++;
            }
        }



        public GameObject FindNearestObject()
        {
            var hits = Physics.SphereCastAll(this.transform.position, 5.0f, transform.forward);
            foreach (var hit in hits)
            {
                var weapon = hit.collider.GetComponentInParent<Interactable>();
                if (weapon != null)
                {
                    return weapon.gameObject;
                }
            }
            return null;
        }
    }
}
