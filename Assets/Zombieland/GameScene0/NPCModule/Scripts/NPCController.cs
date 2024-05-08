using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCAnimationModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCImpactableSensorModule;
using Zombieland.GameScene0.NPCModule.NPCMovingModule;
using Zombieland.GameScene0.NPCModule.NPCSpawnModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public class NPCController : Controller, INPCController
    {
        public INPCManagerController NPCManagerController { get; private set; }
        public NPCSpawnData NPCSpawnData { get; private set; }
        public INPCDataController NPCDataController { get; private set; }
        public INPCVisualBodyController NPCVisualBodyController { get; private set; }
        public INPCSpawnController NPCSpawnController { get; private set; }
        public INPCImpactableSensorController NPCImpactableSensorController { get; private set; }
        public INPCMovingController NPCMovingController { get; private set; }
        public INPCAnimationController NPCAnimationController { get; private set; }

        public NPCController(IController parentController, List<IController> requiredControllers, NPCSpawnData npcSpawnData) : base(parentController, requiredControllers)
        {
            NPCManagerController = parentController as INPCManagerController;
            NPCSpawnData = npcSpawnData;
        }


        protected override void CreateHelpersScripts()
        {
            // This controller doesn�t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            NPCDataController = new NPCDataController(this, null);
            subsystemsControllers.Add((IController) NPCDataController);

            NPCVisualBodyController = new NPCVisualBodyController(this, new List<IController> { (IController)NPCManagerController.RootController.EnvironmentController });
            subsystemsControllers.Add((IController)NPCVisualBodyController);

            NPCSpawnController = new NPCSpawnController(this, new List<IController> { (IController)NPCDataController, (IController)NPCVisualBodyController });
            subsystemsControllers.Add((IController)NPCSpawnController);

            NPCImpactableSensorController = new NPCImpactableSensorController(this, new List<IController> { (IController)NPCVisualBodyController });
            subsystemsControllers.Add((IController)NPCImpactableSensorController);

            NPCMovingController = new NPCMovingController(this, new List<IController> { (IController)NPCVisualBodyController, (IController)NPCSpawnController });
            subsystemsControllers.Add((IController)NPCMovingController);

            NPCAnimationController = new NPCAnimationController(this, new List<IController> { (IController)NPCMovingController });
            subsystemsControllers.Add((IController)NPCAnimationController);
        }
    }
}