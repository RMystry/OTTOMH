using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class WeaponPrefabHandler : MonoBehaviour
    {
        GameObject instantiatedObject;
        public void InstantiateWeaponPrefab(GameObject weaponPrefab)
        {
            Destroy(instantiatedObject);

            instantiatedObject = Instantiate(weaponPrefab, transform);
        }

        public bool UseWeapon<T, T2>(Vector3 pos, T2 descriptor, out Collider[] collisions) where T : WeaponBase<T2> where T2 : WeaponDescriptor
        {
            var weaponBase = instantiatedObject.GetComponent<T>();
            if(weaponBase != null)
            {
                return weaponBase.Attack(pos, descriptor, out collisions);
            }
            else
            {
                collisions = null;
                return false;
            }
        }
    }
}
