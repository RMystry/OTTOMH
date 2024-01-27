using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Gameplay/Weapons/Arena Effect", fileName = "New Arena Effect")]
    public class ArenaEffect : WeaponDescriptor
    {
        [Header("SPECIAL FX")]
        public bool AreaOfEffect;
        public float duration;
    }
}
