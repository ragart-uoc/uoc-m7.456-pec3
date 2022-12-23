using System.Collections;
using UnityEngine;

namespace PEC3.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public CanvasGroup logoGroup;
        public CanvasGroup menuGroup;
        
        private IEnumerator Start()
        {
            // Set alpha to 0 for logo and main menu
            logoGroup.alpha = 0;
            menuGroup.alpha = 0;
            
            // Disable menu buttons
            menuGroup.interactable = false;

            StartCoroutine((Fade.FadeTo(logoGroup, "alpha", 1, 2.5f, () => {})));
            yield return new WaitForSeconds(1.5f);
            
            StartCoroutine(Fade.FadeTo(menuGroup, "alpha", 1, 1.5f, () => { menuGroup.interactable = true; }));
            
        }
    }
}
