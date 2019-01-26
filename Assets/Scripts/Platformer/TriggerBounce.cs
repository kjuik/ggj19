using UnityEngine;

namespace Platformer
{
    public class TriggerBounce : Trigger
    {
        [SerializeField] float bounceImpulse = 5f;
        
        public override void Execute()
        {
            Player.Instance.Bounce(bounceImpulse);
        }
    }
}