using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    /// <summary>
    /// Will handle the basic stuff between the player and the game.
    /// </summary>
    [AddComponentMenu("GGJ/Game Manager")]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get => m_instance; }
        private static GameManager m_instance;

        public static GameObject Player { get => m_playerInstance; }
        private static GameObject m_playerInstance;

        [SerializeField]
        private PlayerInputHandler m_inputHandler;

        [SerializeField]
        private CinemachineVirtualCamera m_cinemachineVCamera;
        
        [Header("Settings")]
        [SerializeField]
        private GameObject m_playerPrefab;
        [SerializeField]
        private Transform m_cameraLookAtTarget;




        private void Awake()
        {
            if(m_instance == null)
            {
                m_instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // we already have an instance of a GameManager, so we'll destroy this.
                Destroy(gameObject);
            }


            if(Camera.main.GetComponent<CinemachineBrain>() == null)
            {
                Camera.main.gameObject.AddComponent<CinemachineBrain>();
            }
        }



        public bool SpawnPlayer(Vector3 spawnLocation)
        {
            if(m_playerPrefab == null)
            {
                Debug.LogWarning("Player Prefab Not Set");
                return false;
            }

            if(m_playerInstance != null)
            {
                Debug.LogWarning("Player Instance has already been instantiated.");
                m_playerInstance.transform.position = spawnLocation;
                return false;
            }

            // we've checked the other possibilities, so now we can spawn the player and set the instance.
            m_playerInstance = Instantiate(m_playerPrefab, spawnLocation, Quaternion.identity);
            m_playerInstance.transform.parent = null;
            m_playerInstance.name = "Player";


            m_cameraLookAtTarget.GetComponent<FollowTarget>().target = m_playerInstance.transform;


            GetComponent<PlayerLook>().SetPlayerTarget(m_playerInstance.transform);

            if(m_playerInstance.GetComponent<PlayerHandler>() != null)
            {
                RegisterPlayerHandler(m_playerInstance.GetComponent<PlayerHandler>());
                return true;
            }
            else
            {
                Debug.LogWarning("Player Prefab does not have required component PlayerHandler");
                return false;
            }
        }

        public void RegisterPlayerHandler(PlayerHandler handler)
        {
            // We will handle everything regarding the player (events and whatnot) here. Let's start with the player input events.

            SetupPlayerInputs(handler);
            handler.Register();
        }

        private void SetupPlayerInputs(PlayerHandler handler)
        {
            if(m_inputHandler == null)
            {
                Debug.LogError("Player Input Handler Is Not Set Up!");
                return;
            }
            handler.ConnectPlayerMovementToInputs(m_inputHandler);
            handler.SetupCombatInputs(m_inputHandler);

        }


    }
}
