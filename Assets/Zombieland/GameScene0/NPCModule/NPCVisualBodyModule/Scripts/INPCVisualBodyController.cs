using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public interface INPCVisualBodyController
    {
        GameObject NPCInScene { get; }
        GameObject WeaponInScene { get; }
        List<GameObject> SensorTriggersGameobject { get; }
        INPCController NPCController { get; }
    }
}