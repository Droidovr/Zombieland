using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class NPCVisionSensorController : Controller, INPCVisionSensorController
    {
        private readonly INPCController NPCController;
        

        public NPCVisionSensorController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NPCController = (INPCController) parentController;
            CreateHelpersScripts();
        }

        protected override void CreateHelpersScripts()
        {
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
        }
    }
}
