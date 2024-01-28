using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
namespace GGJ
{
    public class ItemSpawning : MonoBehaviour
    {
        public GameObjectList listOfSpawnableWeapons;


        public bool spawnItems;
        public float SpawnRate = 2f;

        float timeElapsed = 0;


        private void Start()
        {
            spawnItems = true;
            timeElapsed = 0;

        }

        private void Update()
        {
            
            if (!spawnItems) return;

            if (timeElapsed <= 0)
            {
                // spawn item
                SpawnWeaponOnPlayer();


                timeElapsed = SpawnRate;
            }


            timeElapsed -= Time.deltaTime;

        }

        private void SpawnWeaponOnPlayer()
        {
            Debug.Log("SPAWN ITEM!");
            var randomItem = listOfSpawnableWeapons.PickRandom();


            Vector3 pos = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

            Debug.Log($"Spawned: {randomItem} at {pos} relative to player");
            var GO = Instantiate(randomItem, GameManager.Player.transform.position + pos, Quaternion.identity);

            GO.transform.position = GameManager.Player.transform.position;
        }
    }
}
