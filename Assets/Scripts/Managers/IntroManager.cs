using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace PEC3.Managers
{
    /// <summary>
    /// Class <c>IntroManager</c> contains the methods and properties needed for the intro sequence.
    /// </summary>
    public class IntroManager : MonoBehaviour
    {
        /// <value>Property <c>background</c> represents the Transform component containing the background images.</value>
        public Transform background;
        
        /// <value>Property <c>screenText</c> represents the UI element containing the intro text.</value>
        public TextMeshProUGUI screenText;
        
        /// <value>Property <c>_cameraAudioSource</c> represents the AudioSource component of the camera.</value>
        private AudioSource _cameraAudioSource;
        
        /// <value>Property <c>AudioClips</c> represents a dictionary containing all sounds and music for the game.</value>
        private readonly Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
        
        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _audioClips.Add("music-intro", Resources.Load<AudioClip>("Music/theme-myst"));
            _audioClips.Add("music-menu", Resources.Load<AudioClip>("Music/theme-fall_of_arcana"));
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private IEnumerator Start()
        {
            // Get the main camera source
            _cameraAudioSource = Camera.main.GetComponent<AudioSource>();
            _cameraAudioSource.clip = _audioClips.TryGetValue("music-intro", out var clipgame) ? clipgame : null;
            _cameraAudioSource.Play();
            
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
                StartCoroutine(Fade.FadeColorAlpha(bgLayerRenderer, 255, 2.5f));
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

        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.anyKeyDown || Input.touchCount > 0)
            {
                LoadMainMenu();
            }
        }

        /// <summary>
        /// Method <c>LoadMainMenu</c> loads the main menu scene.
        /// </summary>
        private static void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
