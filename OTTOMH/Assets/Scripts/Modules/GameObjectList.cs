using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    [CreateAssetMenu(menuName = "Architecture/Global Variables/Game Object List", fileName = "New Gmae Object List")]
    public class GameObjectList : BaseVariable<List<GameObject>>
    {
        public int Length { get { return Value.Count; } }
        public GameObject this[int index]
        {
            get
            {
                return Value[index];
            }
        }


        public GameObject PickRandom()
        {
            var choose = Random.Range(0, Length);

            return this[choose];
        }
    }



}
