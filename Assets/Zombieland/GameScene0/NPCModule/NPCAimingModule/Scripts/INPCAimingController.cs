using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCAimingController
{
    public interface INPCAimingController
    {
        INPCController NPCController { get; }

        Transform GetTarget();
    }
}