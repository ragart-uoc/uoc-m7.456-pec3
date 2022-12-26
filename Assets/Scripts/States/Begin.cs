using System.Collections;
using UnityEngine;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>Begin</c> contains the logic for the Begin state.
    /// </summary>
    public class Begin : State
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public Begin(GameManager gameManager) : base(gameManager)
        {
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        public override IEnumerator Start()
        {
            // Show initial message
            GameManager.generalText.text = "Player order:\n" + string.Join("\n", GameManager.playerOrder);
            GameManager.generalText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(5f);
            GameManager.generalText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            
            // Start first turn
            GameManager.SetState(new PlayerTurn(GameManager));
        }
    }
}
