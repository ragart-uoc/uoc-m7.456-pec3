using System.Collections;
using PEC3.Managers;
using UnityEngine;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>End</c> contains the logic for the ShotsFired state.
    /// </summary>
    public class End : State
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public End(GameManager gameManager) : base(gameManager)
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
