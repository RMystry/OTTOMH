using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Gameplay/Weapons/Ranged", fileName = "New Ranged Weapon")]
    public class RangedWeapon : WeaponDescriptor
    {
        [Space]
        [Header("Ranged Weapon Settings")]
        public int shotCount;
        public float spread;
    }
}
