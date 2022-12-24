using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    public class PlayerTurn : State
    {
        public PlayerTurn(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            yield break;
        }
    }
}
