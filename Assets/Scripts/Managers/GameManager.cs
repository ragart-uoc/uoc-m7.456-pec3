using UnityEngine;
using UnityEngine.SceneManagement;
using PEC3.Entities;

namespace PEC3.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> contains the methods and properties needed for the game.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <value>Property <c>Instance</c> represents the instance of the GameplayManager.</value>
        public static GameManager Instance;
        
        /// <value>Property <c>_players</c> represents the game players.</value>
        private Player[] _players;

        const int MinPlayers = 2;
        const int MaxPlayers = 4;

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private void Start()
        {
            
        }

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        /// <summary>
        /// Method <c>NewGame</c> starts a new game.
        /// </summary>
        public void NewGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        /// <summary>
        /// Method <c>RestartGame</c> destroys the instance and returns to the player selection screen.
        /// </summary>
        public void RestartGame()
        {
            Destroy(this.gameObject);
            Instance = null;
            SceneManager.LoadScene("NewGame");
        }
        
        /// <summary>
        /// Method <c>ExitGame</c> destroys the instance and returns to the main menu screen.
        /// </summary>
        public void ExitGame()
        {
            Destroy(this.gameObject);
            Instance = null;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
