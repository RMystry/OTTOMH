using System.Collections;
using System.Collections.Generic;
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
    }


    [CreateAssetMenu(menuName = "Gameplay/Weapons/Arena Effect", fileName = "New Arena Effect")]
    public class ArenaEffect : WeaponDescriptor
    {
        [Header("SPECIAL FX")]
        public bool AreaOfEffect;
        public float duration;
    }


    public enum WeaponClass
    {
        SQUISHY,    // slowy sticky thing
        GUN,        // shooty mc shoot shoot
        EXPLOSIVE,  // boom boom
        STICK,      // swingy stick thing
        TRAUMA      // wildcard
    }
}
