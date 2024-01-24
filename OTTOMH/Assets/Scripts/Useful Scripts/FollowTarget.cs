using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;


        public bool useFixedUpdate;



        private void Update()
        {
            if (useFixedUpdate) return;

            gameObject.transform.position = target.position;
        }

        private void FixedUpdate()
        {
            if(!useFixedUpdate) return;

            gameObject.transform.position = target.position;
        }
    }
}
