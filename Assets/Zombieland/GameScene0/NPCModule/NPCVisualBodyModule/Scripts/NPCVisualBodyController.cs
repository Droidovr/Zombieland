using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NpcVisualBodyController : Controller, INpcVisualBodyController
    {
        public GameObject ActiveNPC { get; set; }
        private readonly INpcController _NPCController;

        public NpcVisualBodyController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INpcController) parentController;
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
            var NPCPrefab = Resources.Load<GameObject>(_NPCController.DataController.NPCData.name);
            ActiveNPC = GameObject.Instantiate(NPCPrefab);
            ActiveNPC.SetActive(false);
        }
        
        private void TestCreateSubsystem()
        {
            CreateNPConScene();
        }
    }
}
