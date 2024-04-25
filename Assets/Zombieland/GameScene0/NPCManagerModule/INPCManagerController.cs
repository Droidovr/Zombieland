using UnityEngine;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public interface INpcManagerController
    {
        public IRootController RootController { get; set; }
        public Transform CharacterTransform { get; set; }
    }
}
