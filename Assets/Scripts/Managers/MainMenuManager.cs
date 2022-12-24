using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PEC3.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public CanvasGroup logoGroup;
        public CanvasGroup menuGroup;
        public GameObject credits;
        
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
        
        public void StartGame()
        {
            SceneManager.LoadScene("NewGame");
        }

        public void ToggleCredits()
        {
            credits.SetActive(!credits.activeSelf);
        }
        
        public void QuitGame()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}
