using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace PEC3.Managers
{
    /// <summary>
    /// Class <c>OpeningManager</c> contains the methods and properties needed for the opening sequence.
    /// </summary>
    public class OpeningManager : MonoBehaviour
    {
        /// <value>Property <c>screenText</c> represents the UI element containing the opening text.</value>
        public TextMeshProUGUI screenText;

        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        private IEnumerator Start()
        {
            screenText.canvasRenderer.SetAlpha(0.0f);
            screenText.text = "Salvador Banderas presents";
            screenText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(2.5f);
            screenText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene("Intro");
        }
    }
}
