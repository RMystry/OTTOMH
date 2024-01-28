using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ
{
    public class scoreKeeper : MonoBehaviour
    {
        // Start is called before the first frame update
        public IntVariable score;
        // Update is called once per frame
        void Update()
        {
            gameObject.GetComponentInChildren<TextMesh>().text = "Score: " + score.Value;
        }
    }
}
