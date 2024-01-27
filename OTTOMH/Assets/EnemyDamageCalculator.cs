using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class EnemyDamageCalculator : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            collision.rigidbody.AddForce(transform.forward * 25f, ForceMode.Impulse);

            collision.gameObject.GetComponentInParent<HealthHandler>().TakeDamage(5f);
        }
    }
}
