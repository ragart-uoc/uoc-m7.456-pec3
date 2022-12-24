using System.Collections;
using PEC3.Managers;

namespace PEC3.States
{
    public class ShotsFired : State
    {
        public ShotsFired(GameManager gameManager) : base(gameManager)
        {
        }

        public override IEnumerator Start()
        {
            yield break;
        }
    }
}
