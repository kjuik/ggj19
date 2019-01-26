namespace Platformer
{
    public class TriggerPickUpObject : Trigger
    {
        public override void Execute()
        {
            Player.Instance.UnlockCarryingObject();
            Destroy(gameObject);
        }
    }
}