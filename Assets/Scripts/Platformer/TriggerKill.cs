using UnityEngine;

namespace Platformer
{
    public class TriggerKill : Trigger
    {
        [SerializeField] AudioClip sfx;
        [SerializeField] string gameOverMessage;
        
        public override void Execute()
        {
            TheftManager.Instance.KillPlayer(sfx, gameOverMessage);
        }
    }
}