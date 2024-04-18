namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public interface INPCAwarenessController
    {
        public INPCController NPCController { get; set; }
        public void CanSeeTarget(bool isTargetDetected);
        public void CanHearTarget(bool isTargetDetected);
    }
}
