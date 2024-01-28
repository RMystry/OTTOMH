using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class PlayerAnimationFollower : MonoBehaviour
    {
        [SerializeField]
        public Animator _targetAnimator;

        [SerializeField]
        private Rigidbody _rb;
        [SerializeField]
        private PlayerMovement _pm;

        private void Awake()
        {
            if(_rb == null)
            {
                _rb = GetComponent<Rigidbody>();
            }

            if(_pm == null)
            {
                _pm = GetComponent<PlayerMovement>();
            }
        }


        private void FixedUpdate()
        {
            Vector3 velocity = (_rb.velocity.normalized) * (_rb.velocity.magnitude / _pm.TopSpeed);

            _targetAnimator.SetFloat("x", velocity.x);
            _targetAnimator.SetFloat("z", velocity.z);
        }
    }
}
