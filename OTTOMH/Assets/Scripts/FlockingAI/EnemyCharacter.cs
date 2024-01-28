using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [RequireComponent(typeof(HealthHandler), typeof(AttackHandler), typeof(Chmicken))]
    public class EnemyCharacter : MonoBehaviour
    {
        public HealthHandler _healthHandler;
        public AttackHandler _attackHandler;
        public EnemyRagdollHandler _enemyRagdollHandler;
        public Chmicken MovementHandler;
        public IntVariable intVariable;
        public int pollingRate = 10;
        public GameObject nearestObject;
        public TextMesh Score;
        public int polling = 0;
        // Start is called before the first frame update
        void Start()
        {
            _healthHandler = GetComponent<HealthHandler>();
            _attackHandler = GetComponent<AttackHandler>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Attack()
        {
            _attackHandler.CommandAttack(Vector3.zero);
        }

        public void ApplyEffects()
        {
            //Apply status effects to all the stats
        }

        public void StartRagDoll(ForceInfo force)
        {
            if (_healthHandler.currentHealth <= 0)
            {
                _enemyRagdollHandler.OnDeath(force);
            }
            else
            {
                _enemyRagdollHandler.startRagdoll(force);
            }
        }

        public void OnDeath()
        {
            intVariable.Value += 50;
            ForceInfo force;
            //_enemyRagdollHandler.OnDeath(force);
        }

        private void LateUpdate()
        {
            if (polling == pollingRate)
            {
                this.GetComponent<Chmicken>().GoalObject = FindNearestObject();
                nearestObject = this.GetComponent<Chmicken>().GoalObject;
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
