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
        INPCDataController DataController { get; set; }
        INPCSpawnController SpawnController { get; set; }
        INPCVisualBodyController VisualBodyController { get; set; }
        INPCMovingController MovingController { get; set; }
        INPCVisionSensorController VisionSensorController { get; set; }
        INPCHearingSensorController HearingSensorController { get; set; }
        INPCTakeImpactController TakeImpactController { get; set; }
    }
}
