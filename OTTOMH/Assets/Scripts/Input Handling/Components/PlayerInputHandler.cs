using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GGJ
{
    [AddComponentMenu("GGJ/Player/Player Input Handler")]
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInputActions m_inputActions;

        [Header("Events")]
        [Tooltip("Activate if you want to use some custom inspector events.")]
        [SerializeField]
        private bool m_useUnityEvents;

        [SerializeField]
        [Tooltip("Just some extra exposed events, in case we want some other behaviors")]
        private PlayerInputUnityEventsPanel m_UnityEvents;

        [Header("Debug")]
        [SerializeField]
        private bool m_debugEvents;


        // Regular, subscribable events.
        public delegate void PlayerInputButton(bool isPressed);
        public delegate void PlayerInputVector2(Vector2 inputDir);
        public delegate void PlayerInput();

        public event PlayerInputVector2 OnPlayerInputMove;
        public event PlayerInputButton OnPlayerInputAttack;
        public event PlayerInputButton OnPlayerInputActivate;
        public event PlayerInput OnPlayerInputDodge;
        public event PlayerInput OnPlayerInputDropItem;
        public event PlayerInput OnPlayerInputThrowWeapon;
        public event PlayerInput OnPlayerInputShove;


        #region UNITY METHODS
        private void Awake()
        {
            InitializeInputSystem();
        }

        private void OnEnable()
        {
            InitializeInputSystem();
        }
        private void OnDisable()
        {
            if(m_inputActions != null )
            {
                m_inputActions.gameplay.Disable();
                m_inputActions.Disable();
            }
        }
        #endregion

        private void InitializeInputSystem()
        {
            // sets up and subscribes to input events.
            if (m_inputActions == null)
            {
                m_inputActions = new PlayerInputActions();
            }

            m_inputActions.Enable();
            m_inputActions.gameplay.Enable();


            m_inputActions.gameplay.move.performed += PlayerMove;
            m_inputActions.gameplay.move.canceled += PlayerMove;

            m_inputActions.gameplay.attack.performed += PlayerAttack;
            m_inputActions.gameplay.attack.canceled += PlayerAttack;

            m_inputActions.gameplay.activate.performed += PlayerActivate;
            m_inputActions.gameplay.activate.canceled += PlayerActivate;

            m_inputActions.gameplay.dodge.performed += PlayerDodge;

            m_inputActions.gameplay.throw_weapon.performed += PlayerThrowWeapon;

            m_inputActions.gameplay.drop_item.performed += PlayerDropItem;

            m_inputActions.gameplay.shove.performed += PlayerShove;
        }

        private void PlayerShove(InputAction.CallbackContext ctx)
        {
            if(m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Shove Input");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerShoveInput?.Invoke();
            }

            OnPlayerInputShove?.Invoke();
        }

        private void PlayerDropItem(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Drop Input");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerDropItemInput?.Invoke();
            }
            OnPlayerInputDropItem?.Invoke();
        }

        private void PlayerThrowWeapon(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Throw Input");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerThrowWeaponInput?.Invoke();
            }
            OnPlayerInputThrowWeapon?.Invoke();
        }

        private void PlayerDodge(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Dodge Input");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerDodgeInput?.Invoke();
            }
            OnPlayerInputDodge?.Invoke();
        }

        private void PlayerActivate(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Activate Input {ctx.ReadValueAsButton()}");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerActivateInput?.Invoke(ctx.ReadValueAsButton());
            }
            OnPlayerInputActivate?.Invoke(ctx.ReadValueAsButton());
        }

        private void PlayerAttack(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Attack Input {ctx.ReadValueAsButton()}");
            }

            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerAttackInput?.Invoke(ctx.ReadValueAsButton());
            }
            OnPlayerInputAttack?.Invoke(ctx.ReadValueAsButton());
        }

        private void PlayerMove(InputAction.CallbackContext ctx)
        {
            if (m_debugEvents)
            {
                Debug.Log($"Player Input Handler <> Move Input {ctx.ReadValue<Vector2>()}");
            }
            if (m_useUnityEvents)
            {
                m_UnityEvents.OnPlayerMoveInput?.Invoke(ctx.ReadValue<Vector2>());
            }
            OnPlayerInputMove?.Invoke(ctx.ReadValue<Vector2>());
        }

        /// <summary>
        /// This should show up in the inspector as a drop down.
        /// </summary>
        [Serializable]
        public class PlayerInputUnityEventsPanel
        {
            public UnityEvent<Vector2> OnPlayerMoveInput;
            public UnityEvent<bool> OnPlayerAttackInput;
            public UnityEvent<bool> OnPlayerActivateInput;
            public UnityEvent OnPlayerDodgeInput;
            public UnityEvent OnPlayerShoveInput;
            public UnityEvent OnPlayerDropItemInput;
            public UnityEvent OnPlayerThrowWeaponInput;
        }
    }
}
