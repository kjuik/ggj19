using UnityEngine;

namespace Platformer
{
    public class TriggerExit : Trigger
    {
        public override void Execute()
        {
            TheftManager.Instance.ReachedExit();
        }
    }
}