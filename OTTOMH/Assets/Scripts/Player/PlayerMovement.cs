using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [RequireComponent(typeof(PlayerHandler), typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private float m_topSpeed = 10f;
        [SerializeField]
        private float m_acceleration = 15f;

        [SerializeField]
        private LayerMask m_environmentLayer;

        [SerializeField]
        private float m_dodgeDistance = 3f;

        [SerializeField]
        private float m_dodgeSpeed = 20f;

        [SerializeField]
        private float m_dodgeCooldown = 3f;

        [SerializeField]
        private Vector3 m_dodgeCheckOffset;

        private PlayerHandler m_handler;
        private Rigidbody m_rb;

        private PlayerInputHandler m_inputHandler;

        private Vector3 m_inputDir;
        private Vector3 m_lastNonZeroInputDir;
        private Vector3 m_targetVelocity;


        private Vector3 m_dodgeEndPosition;

        private bool m_canDodge = false;
        private float m_dodgeTimer = 0f;

        public float TopSpeed { get => m_topSpeed; set => m_topSpeed = value; }

        private void Awake()
        {
            m_handler = GetComponent<PlayerHandler>();
            m_rb = GetComponent<Rigidbody>();
            m_canDodge = true;
        }

        private void Update()
        {
            // we're going to calculate the movement here. The Y (up and down movement) should never matter. BUT just in case we ever find ourselves in the air.
            var targetVelocityFromInput = new Vector3(m_inputDir.x, 0, m_inputDir.z); 
            m_targetVelocity = Vector3.Lerp(m_rb.velocity, targetVelocityFromInput * m_topSpeed, Time.deltaTime * m_acceleration);


            if(Physics.Raycast(transform.position, -transform.up, out var groundHit, 500000f, m_environmentLayer))
            {
                if(Vector3.Distance(transform.position, groundHit.point) > 0.01f)
                {
                    transform.position = groundHit.point;
                }
            }

            if(Physics.Raycast(transform.position + m_dodgeCheckOffset, m_lastNonZeroInputDir, out var dodgeHit, m_dodgeDistance))
            {
                m_dodgeEndPosition = dodgeHit.point - m_dodgeCheckOffset;

                // so we know where we're going to hit, but now we need to adjust it for that contact point.
            }
            else
            {
                m_dodgeEndPosition = transform.position + m_lastNonZeroInputDir * m_dodgeDistance;
            }



            if(m_dodgeTimer > 0f && m_canDodge == false)
            {
                m_dodgeTimer -= Time.deltaTime;

                if(m_dodgeTimer <= 0f)
                {
                    m_dodgeTimer = 0f;
                    m_canDodge = true;
                }
            }

        }

        private void FixedUpdate()
        {
            // we're going to apply the movement here. Since its applied to physics.
            m_rb.velocity = m_targetVelocity;
        }



        #region INPUTS        
        public void Enable(PlayerInputHandler handler)
        {
            m_inputHandler = handler;
            m_inputHandler.OnPlayerInputMove += InputMove;
            m_inputHandler.OnPlayerInputDodge += InputDodge;
        }
        public void Disable()
        {
            m_inputHandler.OnPlayerInputMove -= InputMove;
            m_inputHandler.OnPlayerInputDodge -= InputDodge;
            m_inputHandler = null;
        }
        
        private void InputMove(Vector2 input)
        {
            // we normalize the vector to keep diagonal movement on par to the normal directional movement. Otherwise we get funny haha speedy diagonals.
            m_inputDir = new Vector3(input.x, 0, input.y).normalized;

            if(m_inputDir.magnitude > 0)
            {
                m_lastNonZeroInputDir = m_inputDir;
            }
           
        }

        private void InputDodge()
        {
            //Executes the dodge.

            // cancel any velocity right now, and move the set distance.


            StopAllCoroutines();

            StartCoroutine(ExecuteDodge());
            
        }


        private IEnumerator ExecuteDodge()
        {
            if (!m_canDodge) yield break;
            m_canDodge = false;

            m_rb.velocity = Vector3.zero;
            var targetPosition = m_dodgeEndPosition;
            // finish after 3 ticks
            var radius = m_rb.GetComponentInChildren<CapsuleCollider>().radius;
            int framesPassed = 0;
            while(Vector3.Distance(m_rb.position,targetPosition) > radius)
            {
                // if the dodge takes longer than 60 frames, we're going to just stop the dodge right there and call it close enough.
                // We're, ironically, using Update instead of FixedUpdate since we want to make sure that the dodge ends and is responsive. to the player, not
                if(framesPassed > 60)
                {
                    break;
                }
                m_rb.MovePosition(Vector3.Lerp(m_rb.position, targetPosition, Time.deltaTime * m_dodgeSpeed));
                yield return new WaitForEndOfFrame();
                framesPassed++;
            }
            m_dodgeTimer = m_dodgeCooldown;
        }




        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + m_dodgeCheckOffset, m_dodgeEndPosition + m_dodgeCheckOffset);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(m_dodgeEndPosition, 0.24f);
        }

        #endregion
    }
}
