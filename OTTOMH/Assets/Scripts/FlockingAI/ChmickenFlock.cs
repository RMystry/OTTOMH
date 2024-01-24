using Codice.Client.Common.FsNodeReaders;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ
{
    public class ChmickenFlock : MonoBehaviour
    {

        public GameObject enemyPrefab;
        public static int ArenaSize = 25;
        public static int numberOfEnemies = 25;
        public static GameObject[] allEnemys = new GameObject[numberOfEnemies];

        public static Vector3 GoalPos = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 pos = new Vector3 (Random.Range(-ArenaSize, ArenaSize), 0, Random.Range(-ArenaSize, ArenaSize));
                allEnemys[i] = Instantiate(enemyPrefab, pos, Quaternion.identity);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Random.Range(0, 10000) < 50)
            {
               // GoalPos = new Vector3(Random.Range(-ArenaSize, ArenaSize), 0, Random.Range(-ArenaSize, ArenaSize));
            }
        }
    }
}
