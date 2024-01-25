using UnityEngine;
using UnityEngine.Events;

namespace GGJ
{
    [AddComponentMenu("Architecture/Global Event System/Game Event Listener")]
    public class GameEventListener : MonoBehaviour
    {
        public GlobalEvent Event;
        public UnityEvent Response;

        private void OnEnable()
        { Event.RegisterListener(this); }


        public void OnDisable()
        { Event.UnregisterListener(this); }

        public void OnEventRaised()
        { Response.Invoke(); }
    }
}
