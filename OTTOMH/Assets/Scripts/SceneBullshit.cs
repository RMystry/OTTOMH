using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ
{
    public class SceneBullshit : MonoBehaviour
    {
        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Test Scene");
        }
    }
}
