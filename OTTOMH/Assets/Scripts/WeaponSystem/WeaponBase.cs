using UnityEngine;

namespace GGJ
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponDescriptor
    {
        public abstract bool Attack(Vector3 position, T descriptor, out Collider[] collision);
    }
}
