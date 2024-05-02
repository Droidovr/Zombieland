using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NpcVisualBodyController : Controller, INpcVisualBodyController
    {
        public GameObject NpcOnScene { get; set; }
        private readonly INpcController _npcController;

        public NpcVisualBodyController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController) parentController;
        }

        protected override void CreateHelpersScripts()
        {
            CreateNpcOnScene();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void CreateNpcOnScene()
        {
            var npcPrefab = Resources.Load<GameObject>(_npcController.NpcDataController.NpcData.name);
            Debug.Log("-------------------------- npcPrefab" + _npcController.NpcDataController.NpcData.name + " - " + npcPrefab);
            NpcOnScene = GameObject.Instantiate(npcPrefab);
            NpcOnScene.SetActive(false);
        }
    }
}
