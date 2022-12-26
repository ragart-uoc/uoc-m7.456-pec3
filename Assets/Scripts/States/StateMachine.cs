using UnityEngine;

namespace PEC3.States
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _state;

        public void SetState(State state)
        {
            _state = state;
            StartCoroutine(_state.Start());
        }

        public void FixedUpdate()
        {
            _state.FixedUpdate();
        }
    }
}