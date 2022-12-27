using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>End</c> contains the logic for the End state.
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
            // Destroy any projectiles or explosion effects in the scene
            foreach (var projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            {
                Object.Destroy(projectile);
            }
            foreach (var explosion in GameObject.FindGameObjectsWithTag("Explosion"))
            {
                Object.Destroy(explosion);
            }

            // Check if any player is active but has no lives left
            foreach (var player in GameManager.Players.Where(player => player.Value.IsActive && player.Value.Health <= 0))
            {
                // Make virtual camera follow the player
                GameManager.virtualCamera.Follow = player.Value.GameObject.transform;
                
                // Set the player to inactive
                player.Value.IsActive = false;
                //player.Value.GameObject.GetComponent<Animator>().SetBool("isDead", true);
            }
            
            // Check if there are at least two active players
            if (GameManager.Players.Count(player => player.Value.IsActive) >= 2)
            {
                // Pass turn to the next player
                GameManager.SetNextPlayer();
                GameManager.SetState(new PlayerTurn(GameManager));
            }
            else
            {
                GameManager.SetState(new Idle(GameManager));
                SceneManager.LoadScene("End");
            }
            yield return null;
        }
    }
}
