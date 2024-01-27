using UnityEngine;

namespace GGJ
{
    public abstract class WeaponBase<T> : MonoBehaviour where T : WeaponDescriptor
    {
        public abstract bool Attack(Vector3 position, T descriptor, out Collider[] collision);
    }

    public class RangedWeapon : WeaponBase<RangedWeaponDescriptor>
    {

        public override bool Attack(Vector3 position, RangedWeaponDescriptor descriptor, out Collider[] collision)
        {
            var shootPosition = GameManager.Player.transform.position + new Vector3(0f, 0.75f, 0f);
            collision = null;
            if (Physics.Raycast(shootPosition, Vector3.forward, descriptor.range))
            {
                return true;
            }

            return false;
        }
    }
}
