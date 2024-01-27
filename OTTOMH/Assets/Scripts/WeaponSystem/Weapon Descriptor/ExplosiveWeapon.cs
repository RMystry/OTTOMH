using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Gameplay/Weapons/Thrown", fileName = "New Thrown Weapon")]
    public class ThrownWeaponDescriptor : WeaponDescriptor
    {
        [Space]
        [Header("Thrown Settings")]
        public bool isExplosive;
        public bool doesKnockback;


        public ThrownType thrownType;



        public float timeDelay;
        public int bounces;

        public GameObject projectile;


        [System.Flags]
        public enum ThrownType
        {
            IMPACT,
            BOUNCE,
            TIMED,
        }
    }
}
