using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>Idle</c> contains the logic for the Idle state.
    /// </summary>
    public class Idle : State
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public Idle(GameManager gameManager) : base(gameManager)
        {
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        public override IEnumerator Start()
        {
            yield break;
        }
    }
}