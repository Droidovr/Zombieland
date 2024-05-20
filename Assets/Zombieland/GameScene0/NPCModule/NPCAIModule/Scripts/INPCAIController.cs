using System;

namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public interface INPCAIController
    {
        event Action SlotNumber1;
        event Action SlotNumber2;
        event Action SlotNumber3;
        event Action SlotNumber4;
        event Action<bool> OnFire;

        INPCController NPCController { get; }
    }
}