using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace GGJ
{
    public class EnemyRigidbodyController : MonoBehaviour
    {
        private class BoneTransforms
        {
            public Vector3 Position { get; set; }
            public Quaternion Rotation { get; set; }
        }


        public enum CurrentState
        {
            NORMAL,
            RAGDOLL,
            STANDINGUP,
            RESETTINGBONES,
            HIT,
        }


        public UnityEvent OnRagdollComplete;
        

        [SerializeField]
        Rigidbody[] m_rigidbodies;
        [SerializeField]
        Animator m_animator;
        [SerializeField]
        private float m_timeToWakeUp;
        [SerializeField]
        private CurrentState m_currentState;

        [SerializeField]
        private string m_standUpFaceStateName;
        [SerializeField]
        private string m_standUpBackStateName;
        [SerializeField]
        private string m_bodyHitStateName;

        [SerializeField]
        private float m_resetBoneTime = 1.35f;

        private Transform m_hipsBone;

        public Rigidbody[] Rigidbodies { get => m_rigidbodies; }


        private BoneTransforms[] m_standupBoneTransforms;
        private BoneTransforms[] m_ragdollBoneTransforms;
        private Transform[] m_bones;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            if (m_currentState == CurrentState.STANDINGUP)
            {
                if (m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_standUpBackStateName) == false || m_animator.GetCurrentAnimatorStateInfo(0).IsName(m_standUpFaceStateName) == false)
                {
                    m_currentState = CurrentState.NORMAL;
                }
            }
        }


        private void Initialize()
        {
            SetupRigidbodies();

            if (m_animator == null)
                m_animator = GetComponent<Animator>();

            m_hipsBone = m_animator.GetBoneTransform(HumanBodyBones.Hips);

            m_bones = m_hipsBone.GetComponentsInChildren<Transform>();
            m_standupBoneTransforms = new BoneTransforms[m_bones.Length];
            m_ragdollBoneTransforms = new BoneTransforms[m_bones.Length];

            for (int i = 0; i < m_bones.Length; i++)
            {
                m_standupBoneTransforms[i] = new BoneTransforms();
                m_ragdollBoneTransforms[i] = new BoneTransforms();
            }



            DisableRagdoll();
            m_currentState = CurrentState.NORMAL;
        }



        public void Kill(ForceInfo info)
        {
            StopAllCoroutines();
            m_timeToWakeUp = 0f;
            info.force *= 2f;
            info.force = Vector3.ClampMagnitude(info.force, 50f);
            EnableRagdollWithForce(info);
        }

        public void TemporaryRagdoll(ForceInfo info)
        {
            // this will temporarily ragdoll for a duration.
            StartCoroutine(RagdollTimer(info));

        }

        public void Hit()
        {
            Debug.Log("Playing Hit");
            m_animator.Play(m_bodyHitStateName, 1);
        }

        public void IncreaseTimer()
        {
            if (m_currentState == CurrentState.RAGDOLL)
            {
                m_timeToWakeUp += UnityEngine.Random.Range(0.25f, 3f);

                m_timeToWakeUp = Mathf.Clamp(m_timeToWakeUp, 0.01f, 10f);
            }
        }

        IEnumerator RagdollTimer(ForceInfo info)
        {
            EnableRagdollWithForce(info);
            m_timeToWakeUp = UnityEngine.Random.Range(2.5f, 10f);
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (m_timeToWakeUp >= 0)
            {
                m_timeToWakeUp -= Time.deltaTime;
                yield return wait;
            }
            DisableRagdoll();
        }

        private void SetupRigidbodies()
        {
            if (m_rigidbodies == null || m_rigidbodies.Length <= 0)
            {
                m_rigidbodies = GetComponentsInChildren<Rigidbody>();
            }
        }

        public void EnableRagdollWithForce(ForceInfo info)
        {
            EnableRagdoll();

            var hitBody = info.hitCollider.GetComponent<Rigidbody>();
            if (info.hitCollider == null)
            {
                hitBody = GetComponent<Rigidbody>();
            }


            hitBody.AddForceAtPosition(info.force, info.point, ForceMode.Impulse);
            hitBody.AddExplosionForce(info.force.magnitude, info.point, 1.5f);
        }

        public void EnableRagdoll()
        {
            m_currentState = CurrentState.RAGDOLL;
            foreach (var rb in m_rigidbodies)
            {
                rb.isKinematic = false;
            }

            m_animator.enabled = false;
        }

        public void DisableRagdoll()
        {
            foreach (var rb in m_rigidbodies)
            {
                rb.isKinematic = true;
            }


            // what direction is the hips facing more to? Floor or ceiling?\


            if (m_currentState == CurrentState.RAGDOLL)
            {
                AlignRotationToHips();
                AlignPositionToHips();

                PopulateBoneTransforms(m_ragdollBoneTransforms);
                float compare = Vector3.Dot(m_hipsBone.forward, Vector3.up);

                Debug.Log($"Hips are facing...{(compare < 0 ? "down" : "up")}");

                // now we can play the different animations and wait for them to transition.
                var animToPlay = compare < 0 ? m_standUpFaceStateName : m_standUpBackStateName;

                PopulateAnimationStartBoneTransforms(animToPlay, m_standupBoneTransforms);

                StartCoroutine(ResetBones(animToPlay));
            }
            else
            {
                m_animator.enabled = true;
                m_currentState = CurrentState.NORMAL;
            }
            OnRagdollComplete.Invoke();
        }

        private IEnumerator ResetBones(string animToPlay)
        {
            float elapsedTime = 0f;
            var Wait = new WaitForEndOfFrame();
            m_currentState = CurrentState.RESETTINGBONES;
            while (elapsedTime < m_resetBoneTime)
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / m_resetBoneTime;

                for (int i = 0; i < m_bones.Length; i++)
                {
                    m_bones[i].localPosition = Vector3.Lerp(m_ragdollBoneTransforms[i].Position, m_standupBoneTransforms[i].Position, percentComplete);
                    m_bones[i].localRotation = Quaternion.Lerp(m_ragdollBoneTransforms[i].Rotation, m_standupBoneTransforms[i].Rotation, percentComplete);
                }


                yield return Wait;
            }

            m_currentState = CurrentState.STANDINGUP;
            m_animator.enabled = true;
            m_animator.Play(animToPlay);
        }

        private void AlignRotationToHips()
        {
            Vector3 originalHipsPosition = m_hipsBone.position;
            Quaternion originalHipsRotation = m_hipsBone.rotation;

            Vector3 desiredDirection = m_hipsBone.up * -1f;
            desiredDirection.y = 0f;
            desiredDirection.Normalize();

            Quaternion fromToRotation = Quaternion.FromToRotation(transform.forward, desiredDirection);
            transform.rotation *= fromToRotation;

            m_hipsBone.position = originalHipsPosition;
            m_hipsBone.rotation = originalHipsRotation;
        }

        private void AlignPositionToHips()
        {
            Vector3 originalHipsPos = m_hipsBone.position;
            transform.position = m_hipsBone.position;

            Vector3 positionOffset = m_standupBoneTransforms[0].Position;
            positionOffset.y = 0;

            positionOffset = transform.rotation * positionOffset;
            transform.position -= positionOffset;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
            {
                transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
            }


            m_hipsBone.position = originalHipsPos;
        }

        private void PopulateBoneTransforms(BoneTransforms[] boneTransforms)
        {
            for (int i = 0; i < m_bones.Length; i++)
            {
                boneTransforms[i].Position = m_bones[i].localPosition;
                boneTransforms[i].Rotation = m_bones[i].localRotation;
            }
        }

        private void PopulateAnimationStartBoneTransforms(string clipName, BoneTransforms[] boneTransforms)
        {
            Vector3 positionbeforeSampling = transform.position;
            Quaternion rotationBeforeSampling = transform.rotation;

            foreach (var clip in m_animator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == clipName)
                {
                    clip.SampleAnimation(gameObject, 0);
                    PopulateBoneTransforms(m_standupBoneTransforms);
                    break;
                }
            }

            transform.position = positionbeforeSampling;
            transform.rotation = rotationBeforeSampling;
        }
    }
}