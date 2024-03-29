using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public class NPCMovingController : Controller, INPCMovingController
    {
        public NPCMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
        }

        protected override void CreateHelpersScripts()
        {
            throw new System.NotImplementedException();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            throw new System.NotImplementedException();
        }
    }
}
