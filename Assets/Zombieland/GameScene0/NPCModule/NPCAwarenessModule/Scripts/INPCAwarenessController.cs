using System;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public interface INpcAwarenessController
    {
        public INpcController NpcController { get; }
        public bool IsTargetInFocus { get; }
        public event Action<bool> OnTargetInFocus;
        public void CanSeeTarget(bool isTargetDetected);
        public void CanHearTarget(bool isTargetDetected);
    }
}
