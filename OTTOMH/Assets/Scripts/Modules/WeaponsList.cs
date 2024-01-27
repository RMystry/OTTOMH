using System.Collections;
using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Architecture/Global Variables/WeaponsList", fileName = "New Weapons List")]
    public class WeaponsList : BaseVariable<WeaponDescriptor[]>
    {
        public int Length { get { return Value.Length; } }
        public WeaponDescriptor this[int index] 
        { 
            get
            {
                return Value[index];
            }
        }
    }



}
