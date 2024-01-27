using UnityEngine;

namespace GGJ
{
    public abstract class WeaponBase : MonoBehaviour
    {
        public abstract bool Attack(Vector3 position, out Collider collision);
    }

    public class RangedWeapon : WeaponBase
    {

        public override bool Attack(Vector3 position, out Collider collision)
        {
            collision = null;
            return false;
        }
    }
}
