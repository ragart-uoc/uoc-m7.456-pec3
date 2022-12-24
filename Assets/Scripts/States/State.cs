using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    public abstract class State
    {
        protected GameManager GameManager;

        public State(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        public virtual IEnumerator Start()
        {
            yield break;
        }
    }
}