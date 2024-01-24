using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    /// <summary>
    /// Acts as the communication point between the player, and all other objects. Event driven af.
    /// </summary>
    public class PlayerHandler : MonoBehaviour
    {
        public bool isRegistered { get; private set; }
        public PlayerMovement PlayerMovement { get => m_playerMovement; }


        [SerializeField]
        private PlayerMovement m_playerMovement;

        private void Awake()
        {
            m_playerMovement = GetComponent<PlayerMovement>();
        }

        public void ConnectPlayerMovementToInputs(PlayerInputHandler inputHandler)
        {
            if (inputHandler == null)
            {
                Debug.LogWarning("Input Handler Is Null.");
                return;
            }
            else if (m_playerMovement == null)
            {
                Debug.LogWarning("PlayerMovement is not found!, attempting to find it.");
                m_playerMovement = GetComponent<PlayerMovement>();
            }

            m_playerMovement.Enable(inputHandler);
        }

        internal void Register()
        {
            if(!isRegistered)
            {
                isRegistered = true;
            }
        }
    }
}
