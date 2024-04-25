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
        public INpcManagerController NPCManagerController { get; set; }
        public INpcDataController DataController { get; set; }
        public INpcSpawnController SpawnController { get; set; }
        public INpcVisualBodyController VisualBodyController { get; set; }
        public INpcMovingController MovingController { get; set; }
        public INpcTakeImpactController TakeImpactController { get; set; }
        public INpcAwarenessController AwarenessController { get; set; }

        public INpcAIController AIController { get; set; }
    }
}
