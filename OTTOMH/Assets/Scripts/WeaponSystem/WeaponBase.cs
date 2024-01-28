using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponDescriptor
    {
        public ParticleSystem particles;
        public UnityEvent OnAttack;
        public abstract bool Attack(Vector3 position, T descriptor, out Collider[] collision);
    }
}
