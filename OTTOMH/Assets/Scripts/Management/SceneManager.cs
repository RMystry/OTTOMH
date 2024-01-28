using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private Transform m_spawnLocation;
        [SerializeField]
        private int m_nextSceneIndex;

        public ChmickenFlock flockManager;

        public void Start()
        {
            GameManager.Instance.SpawnPlayer(m_spawnLocation.position);
            flockManager.CreateFlock();
        }




        public void NextLevel()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(m_nextSceneIndex);
        }
    }
}
