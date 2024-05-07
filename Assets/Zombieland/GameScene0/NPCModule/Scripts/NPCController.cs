using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.NPCModule.NPCDataModule;
using Zombieland.GameScene0.NPCModule.NPCVisualBodyModule;

namespace Zombieland.GameScene0.NPCModule
{
    public class NPCController : Controller, INPCController
    {
        public INPCManagerController NPCManagerController { get; private set; }
        public NPCSpawnData NPCSpawnData { get; private set; }
        public INPCDataController NPCDataController { get; private set; }
        public INPCVisualBodyController NPCVisualBodyController { get; private set; }

        public NPCController(IController parentController, List<IController> requiredControllers, NPCSpawnData npcSpawnData) : base(parentController, requiredControllers)
        {
            NPCManagerController = parentController as INPCManagerController;
            NPCSpawnData = npcSpawnData;
        }


        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            NPCDataController = new NPCDataController(this, null);
            subsystemsControllers.Add((IController) NPCDataController);

            NPCVisualBodyController = new NPCVisualBodyController(this, new List<IController> { (IController)NPCManagerController.RootController.EnvironmentController });
            subsystemsControllers.Add((IController)NPCVisualBodyController);
        }
    }
}