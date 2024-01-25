using Codice.Client.BaseCommands.CheckIn.Progress;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class Chmicken : MonoBehaviour
    {
        public float speed = 1.0f;
        float rotationSpeed = 4.0f;
        Vector3 averageHeading;
        Vector3 averagePosition;
        [SerializeField]float neighbourDistance = 10.0f;
        bool turning = false;
        bool isPlayer;
        bool isLeader;
        // Start is called before the first frame update
        void Start()
        {
            speed = Random.Range(speed, speed * 5);
            if(gameObject.CompareTag("Player"))
            {
                isPlayer = true;
            }
            else if(Random.Range(0, 50) % 2 == 0)
            {
                isLeader = true;
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if(isLeader)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireSphere(transform.position, neighbourDistance);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isPlayer)
            {
                turning = Vector3.Distance(transform.position, Vector3.zero) >= ChmickenFlock.ArenaSize;

                if (turning)
                {
                    Vector3 direction = Vector3.zero - transform.position;
                    transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                    speed = Random.Range(0.5f, 1);
                }
                else
                {
                        ApplyRules();
                }


                transform.Translate(0, 0, Time.deltaTime * speed);
            }
        }

        void ApplyRules()
        {
            List<GameObject> god;
            god = ChmickenFlock.allEnemys;

            Vector3 vCenter = Vector3.zero;
            Vector3 vAvoid = Vector3.zero;
            float gSpeed = 1.0f;

            Vector3 goalPos = ChmickenFlock.GoalPos;

            float dist;
            int groupSize = 0;
            foreach(GameObject go in god)
            {
                if(go != this.gameObject && (go.GetComponent<Chmicken>().isPlayer || go.GetComponent<Chmicken>().isLeader) && !this.isLeader)
                {
                    dist = Vector3.Distance(go.transform.position, this.transform.position);
                    if(dist <= neighbourDistance)
                    {
                        vCenter += go.transform.position;
                        groupSize++;

                        if(dist < 1.0f)
                        {
                           vAvoid = vAvoid + (this.transform.position - go.transform.position);
                        }

                        Chmicken another = go.GetComponent<Chmicken>();
                        gSpeed = gSpeed + another.speed;
                    }
                }
            }

            if (groupSize >  0)
            {
                vCenter = vCenter / groupSize + (goalPos - this.transform.position);
                speed = gSpeed/groupSize;

                Vector3 direction = (vCenter + vAvoid) - this.transform.position;
                if(direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
