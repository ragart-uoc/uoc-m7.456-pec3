using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    public class End : State
    {
        public End(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            yield break;
        }
    }
}
