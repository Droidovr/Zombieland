using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCTakeImpactModule;
using Zombieland.GameScene0.NPCModule.NPCVisionSensorModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public interface INPCController
    {
        public INPCManagerController NPCManagerController { get; set; }
        public INPCDataController DataController { get; set; }
        public INPCSpawnController SpawnController { get; set; }
        public INPCVisualBodyController VisualBodyController { get; set; }
        public INPCMovingController MovingController { get; set; }
        public INPCVisionSensorController VisionSensorController { get; set; }
        public INPCHearingSensorController HearingSensorController { get; set; }
        public INPCTakeImpactController TakeImpactController { get; set; }
    }
}
