namespace Platformer
{
    public class TriggerExit : Trigger
    {
        public override void Execute()
        {
            if (Player.Instance.IsCarryingObjectUnlocked)
            {
                TheftManager.Instance.ReachedExit();
            }
        }
    }
}