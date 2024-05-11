using System;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCVisualModule
{
    public interface INPCVisionController
    {
        event Action<IController, bool> OnVisualDetect;

        INPCAwarenessController NPCAwarenessController { get; }
    }
}