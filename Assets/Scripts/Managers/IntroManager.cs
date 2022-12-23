using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace PEC3.Managers
{
    public class IntroManager : MonoBehaviour
    {
        public Transform background;
        public TextMeshProUGUI screenText;

        private IEnumerator Start()
        {
            // Set alpha to 0 for all background images
            var bgChildren = background.GetComponentInChildren<Transform>();
            foreach (Transform bgLayer in bgChildren)
            {
                var bgLayerRenderer = bgLayer.GetComponent<SpriteRenderer>();
                bgLayerRenderer.color = new Color(1, 1, 1, 0f);
            }
            
            // Set alpha to 0 for canvas text
            screenText.canvasRenderer.SetAlpha(0.0f);
            
            // Fade in background
            foreach (Transform bgLayer in background)
            {
                var bgLayerRenderer = bgLayer.GetComponent<SpriteRenderer>();
                StartCoroutine(Fade.ColorFadeTo(bgLayerRenderer, 255, 2.5f));
            }
            yield return new WaitForSeconds(2.5f);
            
            // Fade in text
            screenText.text = "What started as an argument between two friends, turned into a full blown war.";
            screenText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(5f);
            
            // Fade out text
            screenText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            
            // Fade in text
            screenText.text = "Legions of mindless soldiers rampage across the land, forcing everyone to put onion in their omelettes. They call themselves the Onionites.";
            screenText.text += "\n\n";
            screenText.text += "The Rebel Potato Army, a radical group within the Spanish (Omelette) Inquisition, has been fighting them for centuries.";
            screenText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(7.5f);
            
            // Fade out text
            screenText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            
            // Fade in text
            screenText.text = "Now, two more factions have joined the war.";
            screenText.text += "\n\n";
            screenText.text += "The Pina Colada Cult, a group of acolytes who worship pineapple in pizza.";
            screenText.text += "\n\n";
            screenText.text += "And the Napoli Heritage Preservation Association, who won't stop until that madness stops.";
            screenText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(7.5f);
            
            // Fade out text
            screenText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            
            // Fade in text
            screenText.text = "It's time to end the war.";
            screenText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(5f);
            
            // Fade out text
            screenText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);

            LoadMainMenu();
        }

        private void Update()
        {
            if (Input.anyKeyDown || Input.touchCount > 0)
            {
                LoadMainMenu();
            }
        }

        private static void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
