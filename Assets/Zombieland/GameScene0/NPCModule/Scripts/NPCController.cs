using System.Collections.Generic;
using Zombieland.GameScene0.NPCManagerModule;

namespace Zombieland.GameScene0.NPCModule
{
    public class NPCController : Controller, INPCController
    {
        public INPCManagerController NpcManagerController { get; private set; }

        public NPCController(IController parentController, List<IController> requiredControllers, NPCSpawnData npcSpawnData) : base(parentController, requiredControllers)
        {
            NpcManagerController = parentController as INPCManagerController;
        }


        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}