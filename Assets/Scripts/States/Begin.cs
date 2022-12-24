using System.Collections;
using UnityEngine;
using PEC3.Managers;

namespace PEC3.States
{
    public class Begin : State
    {
        public Begin(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            GameManager.generalText.canvasRenderer.SetAlpha(0.0f);
            GameManager.generalText.text = "Order of play:\n" + string.Join("\n", GameManager.playerOrder);
            GameManager.generalText.CrossFadeAlpha(1.0f, 1.5f, false);
            yield return new WaitForSeconds(5f);
            GameManager.generalText.CrossFadeAlpha(0.0f, 1.5f, false);
            yield return new WaitForSeconds(1.5f);
            GameManager.generalText.text = GameManager.CurrentPlayer.Identifier + " - " + GameManager.CurrentPlayer.Name + "\nIt's your turn";
            GameManager.generalText.canvasRenderer.SetAlpha(1.0f);
        }
    }
}
