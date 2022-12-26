using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>Config</c> contains the logic for the ShotsFired state.
    /// </summary>
    public class Config : State
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public Config(GameManager gameManager) : base(gameManager)
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