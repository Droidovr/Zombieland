using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisionSensorModule
{
    public class VisionSensorController : Controller, IVisionSensorController
    {
        private readonly INPCController NPCController;
        

        public VisionSensorController(IController parentController, List<IController> requiredControllers) 
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
