using UnityEngine;

namespace GGJ
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponDescriptor
    {
        public abstract bool Attack(Vector3 position, T descriptor, out Collider[] collision);
    }

    public class MeleeWeapon : WeaponBase<MeleeWeaponDescriptor>
    {
        public override bool Attack(Vector3 position, MeleeWeaponDescriptor descriptor, out Collider[] collision)
        {
            // this is really just directions. 
            // spherecast in an area ahead of the position.
            collision = new Collider[0];
            return false;
        }
    }
}
