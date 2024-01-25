using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        [SerializeField]
        private InteractableVariable m_currentHoveredInteractable;

        public void OnEnable()
        {
            GetComponent<PlayerInputHandler>().OnPlayerInputActivate += ActivateInteractable;
        }

        public void OnDisable()
        {
            GetComponent<PlayerInputHandler>().OnPlayerInputActivate -= ActivateInteractable;
        }


        public void ActivateInteractable()
        {
            if(m_currentHoveredInteractable.Value != null)
            {
                m_currentHoveredInteractable.Value.Interact();
            }
        }
    }
}
