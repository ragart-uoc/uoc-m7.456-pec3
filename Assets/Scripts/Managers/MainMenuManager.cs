using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC3.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        /// <value>Property <c>logoGroup</c> represents the CanvasGroup component containing the logo images.</value>
        public CanvasGroup logoGroup;
        
        /// <value>Property <c>menuGroup</c> represents the CanvasGroup component containing the main menu entries.</value>
        public CanvasGroup menuGroup;
        
        /// <value>Property <c>creditsGroup</c> represents the CanvasGroup component containing the credits.</value>
        public GameObject credits;
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private IEnumerator Start()
        {
            // Set alpha to 0 for logo and main menu
            logoGroup.alpha = 0;
            menuGroup.alpha = 0;
            
            // Disable menu buttons
            menuGroup.interactable = false;

            StartCoroutine((Fade.FadePropertyValue(logoGroup, "alpha", 1, 2.5f, () => {})));
            yield return new WaitForSeconds(1.5f);
            
            StartCoroutine(Fade.FadePropertyValue(menuGroup, "alpha", 1, 1.5f, () => { menuGroup.interactable = true; }));
        }
        
        /// <summary>
        /// Method <c>StartGame</c> loads the new game scene.
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("NewGame");
        }

        /// <summary>
        /// Method <c>ToggleCredits</c> toggles the visibility of the credits.
        /// </summary>
        public void ToggleCredits()
        {
            credits.SetActive(!credits.activeSelf);
        }
        
        /// <summary>
        /// Method <c>QuitGame</c> quits the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
