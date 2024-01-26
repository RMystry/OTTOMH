using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Gameplay/Weapons/Melee", fileName = "New Melee Weapon")]
    public class MeleeWeapon : WeaponDescriptor
    {
        [Header("Melee Weapon Settings")]
        public float knockBack;
        public bool ragdollsOnHit;
    }
}
