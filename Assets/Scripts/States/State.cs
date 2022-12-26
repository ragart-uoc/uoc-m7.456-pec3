using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    public abstract class State
    {
        protected readonly GameManager GameManager;

        protected State(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        public virtual IEnumerator Start()
        {
            yield break;
        }
        public virtual void FixedUpdate()
        {
        }
    }
}