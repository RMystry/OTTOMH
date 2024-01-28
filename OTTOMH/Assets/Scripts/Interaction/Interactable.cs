using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    [RequireComponent(typeof(Outline))]
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        private InteractableRuntimeSet m_interactablesSet;

        [SerializeField]
        private float m_maxInteractionDistance = 2.5f;

        [SerializeField]
        private UnityEvent OnInteract;

        public void Start()
        {
            // register with interactables set.
            m_interactablesSet.Add(this);

            GetComponent<Outline>().enabled = false;
        }
        public void OnEnable()
        {
            m_interactablesSet.Add(this);
        }
        public void OnDisable()
        {
            m_interactablesSet.Remove(this);
        }
        public void OnDestroy()
        {
            m_interactablesSet.Remove(this);
        }

        public bool GetIsInRange(out float dist)
        {
            dist = Vector3.Distance(GameManager.Player.transform.position, transform.position);

            if (dist <= m_maxInteractionDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void Interact(GameObject interactionSource)
        {
            // interactable range. 
            if(GetIsInRange(out var dist))
            {
                OnInteract?.Invoke();
                Debug.Log($"Activated {gameObject.name}");
            }
        }

        public void Hover(bool hovering)
        {
            GetComponent<Outline>().enabled = hovering;
        }

        private void OnValidate()
        {
            // set the interaction layer.
            if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
            {
                gameObject.layer = LayerMask.NameToLayer("Interactable");


                gameObject.GetComponentInChildren<Collider>().gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, m_maxInteractionDistance);
        }

    }
}
