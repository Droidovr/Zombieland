using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public interface INPCManagerController
    {
        public IRootController RootController { get; set; }
        public Transform CharacterTransform { get; set; }
    }
}
