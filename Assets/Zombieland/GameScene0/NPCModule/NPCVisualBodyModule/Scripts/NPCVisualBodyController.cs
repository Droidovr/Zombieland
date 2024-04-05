using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCVisualBodyController : Controller, INPCVisualBodyController
    {
        public GameObject ActiveNPC { get; set; }
        private readonly INPCController NPCController;

        public NPCVisualBodyController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            NPCController = (INPCController) parentController;
            CreateHelpersScripts();
        }

        protected override void CreateHelpersScripts()
        {
            CreateNPC();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void CreateNPC()
        {
            var NPCPrefab = Resources.Load<GameObject>(NPCController.DataController.NPCData.Name);
            ActiveNPC = GameObject.Instantiate(NPCPrefab);
            ActiveNPC.SetActive(false);
        }
    }
}
