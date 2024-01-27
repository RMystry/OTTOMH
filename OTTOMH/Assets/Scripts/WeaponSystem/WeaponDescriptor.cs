using UnityEngine;

namespace GGJ
{
    public abstract class WeaponDescriptor : ScriptableObject
    {
        [Header("Base Settings")]
        public string weaponName;
        public string weaponDescription;
        public WeaponClass weaponClass;
        public float damage;
        public float range;
        public float attackSpeed;
        public GameObject weaponPrefab;

        [Header("Weapon Combo")]
        public WeaponComponents components;
    }
}
