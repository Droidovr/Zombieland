using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public interface IVisualBodyController
    {
        GameObject CharacterInScene { get; }
        List<GameObject> SensorTriggersGameobject { get; }
    }
}