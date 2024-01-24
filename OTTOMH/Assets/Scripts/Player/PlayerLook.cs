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
        private GameObject targetInteractable;

        private RaycastHit interactableHit;

        private Vector3 environmentLookPosition;

        [SerializeField]
        private bool debug;

        private void Awake()
        {
            if(targetCamera == null )
            {
                targetCamera = Camera.main;
            }
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
                targetInteractable = interactableHit.collider.gameObject;
            }
            else
            {
                targetInteractable = null;
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
