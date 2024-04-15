using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCVisualBodyController : Controller, INPCVisualBodyController
    {
        public GameObject ActiveNPC { get; set; }
        private readonly INPCController _NPCController;

        public NPCVisualBodyController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INPCController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            CreateNPConScene();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void CreateNPConScene()
        {
            var NPCPrefab = Resources.Load<GameObject>(_NPCController.DataController.NPCData.Name);
            ActiveNPC = GameObject.Instantiate(NPCPrefab);
            ActiveNPC.SetActive(false);
        }
        
        private void TestCreateSubsystem()
        {
            CreateNPConScene();
        }
    }
}
