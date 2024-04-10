using System.Collections.Generic;

namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts
{
    public class NPCHearingSensorController : Controller, INPCHearingSensorController
    {
        public NPCHearingSensorController(IController parentController, List<IController> requiredControllers)
            : base(parentController, requiredControllers)
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
