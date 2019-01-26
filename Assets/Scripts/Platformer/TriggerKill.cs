using UnityEngine;

namespace Platformer
{
    public class TriggerKill : Trigger
    {
        public override void Execute()
        {
            TheftManager.Instance.KillPlayer();
        }
    }
}