using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ
{
    public class ChmickenFlock : MonoBehaviour
    {

        public GameObject enemyPrefab;
        [SerializeField] public static int ArenaSize = 30;
        [SerializeField] public int numberOfEnemies = 25;
        [SerializeField] public static int numberOfLeaders = 3;
        public static List<GameObject> allEnemys = new List<GameObject>();

        public static Vector3 GoalPos = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
           
        }

        public void CreateFlock()
        {
            allEnemys.Add(GameManager.Player);
            for (int i = 0; i < numberOfEnemies; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-ArenaSize, ArenaSize), 0, Random.Range(-ArenaSize, ArenaSize));
                allEnemys.Add(Instantiate(enemyPrefab, pos, Quaternion.identity));
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
