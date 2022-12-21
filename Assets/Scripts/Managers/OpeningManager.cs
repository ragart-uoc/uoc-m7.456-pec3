using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace PEC3.Managers
{
    public class OpeningManager : MonoBehaviour
    {
        public TextMeshProUGUI screenText;

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
