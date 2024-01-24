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



        private PlayerHandler m_handler;
        private Rigidbody m_rb;

        private PlayerInputHandler m_inputHandler;

        private Vector3 m_inputDir;

        private Vector3 m_targetVelocity;

        private void Awake()
        {
            m_handler = GetComponent<PlayerHandler>();
            m_rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // we're going to calculate the movement here. The Y (up and down movement) should never matter. BUT just in case we ever find ourselves in the air.
            var targetVelocityFromInput = new Vector3(m_inputDir.x, m_rb.velocity.y, m_inputDir.z); 
            m_targetVelocity = Vector3.Lerp(m_rb.velocity, targetVelocityFromInput * m_topSpeed, Time.deltaTime * m_acceleration);
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
           
        }

        private void InputDodge()
        {
            // moves a set distance towards the forward direction.
        }

        #endregion
    }
}
