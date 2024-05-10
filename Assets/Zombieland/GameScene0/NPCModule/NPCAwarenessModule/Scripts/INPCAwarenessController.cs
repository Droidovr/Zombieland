using System;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule
{
    public interface INPCAwarenessController
    {
        event Action<IController> OnHearingSound;

        INPCController NPCController { get; }
        INPCHearingController NPCHearingController { get; }
    }
}