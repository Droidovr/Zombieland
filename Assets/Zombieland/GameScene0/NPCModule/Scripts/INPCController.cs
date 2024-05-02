using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.AIModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCTakeImpactModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public interface INpcController
    {
        public INpcManagerController NpcManagerController { get; }
        public INpcDataController NpcDataController { get; }
        public INpcSpawnController NpcSpawnController { get; }
        public INpcVisualBodyController NpcVisualBodyController { get; }
        public INpcMovingController NpcMovingController { get; }
        public INpcTakeImpactController NpcTakeImpactController { get; }
        public INpcAwarenessController NpcAwarenessController { get; }
        public INpcAIController NpcAIController { get; set; }
    }
}
