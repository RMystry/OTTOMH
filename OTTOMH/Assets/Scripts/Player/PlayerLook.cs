using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GGJ
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        private LayerMask environmentLayer, interactableLayer;

        [SerializeField]
        private InteractableVariable m_targetInteractable;
        [SerializeField]
        private GlobalEvent OnInteractableHovered;
        [SerializeField]
        private GlobalEvent OnInteractableNoLongerHovered;

        private RaycastHit interactableHit;

        private Vector3 environmentLookPosition;

        [SerializeField]
        private bool debug;

        private Transform playerTarget;


        private void Start()
        {
            if(targetCamera == null )
            {
                targetCamera = Camera.main;
            }
        }


        public void SetPlayerTarget(Transform playerTarget)
        {
            this.playerTarget = playerTarget;
        }

        private void Update()
        {
            var mousePos = Mouse.current.position.value;
            var ray = targetCamera.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y));

            if (Physics.Raycast(ray, out var environmentHit, 1000f, environmentLayer))
            {
                environmentLookPosition = environmentHit.point;
            }
            if (Physics.Raycast(ray, out interactableHit, 1000f, interactableLayer))
            {
                if(m_targetInteractable.Value == null)
                {
                    if (interactableHit.transform.parent != null)
                    {
                        m_targetInteractable.SetValue(interactableHit.transform.parent.GetComponent<Interactable>());
                    }
                    else
                    {
                        m_targetInteractable.SetValue(interactableHit.collider.GetComponent<Interactable>());
                    }

                    m_targetInteractable.Value.Hover(true);
                    OnInteractableHovered.Raise();
                }
            }
            else if (m_targetInteractable.Value != null)
            {
                OnInteractableNoLongerHovered.Raise();
                m_targetInteractable.Value.Hover(false);


                m_targetInteractable.SetValue(null);
            }


            // now we need to have the player look at the position. Let's see if this works.

            if (playerTarget != null)
            {
                playerTarget.LookAt(environmentLookPosition);
            }
        }



        private void OnDrawGizmos()
        {
            if (targetCamera == null)
            { return; }

            if (debug)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(targetCamera.transform.position, environmentLookPosition);
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(environmentLookPosition, 0.05f);
                Gizmos.DrawLine(environmentLookPosition, environmentLookPosition + Vector3.up);
                
                
                Gizmos.color = Color.green;
                Gizmos.DrawLine(interactableHit.point, interactableHit.point + interactableHit.normal);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(interactableHit.point, 0.2f);
            }
        }
    }
}
