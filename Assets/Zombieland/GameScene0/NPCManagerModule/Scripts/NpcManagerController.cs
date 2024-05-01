using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCManagerModule.Scripts;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NpcManagerController : Controller, INpcManagerController
    {
        public IRootController RootController { get; }
        public Transform CharacterTransform { get; }
        public List<INpcController> ActiveNpcControllers { get; }
        
        public NpcManagerController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            RootController = (IRootController)parentController;
            CharacterTransform = RootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            ActiveNpcControllers = new List<INpcController>();
        }

        protected override void CreateHelpersScripts()
        {
            //This method has no implementation
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            var npcSearch = new GameObject("Search").AddComponent<NpcSearch>();
            var npcSpawnDataList = npcSearch.GetNpcSpawnDataList();
            foreach (var npcSpawnData in npcSpawnDataList)
            {
                var npcController = new NpcController(this, null, npcSpawnData);
                AddNpcToActive(npcController);
            }
        }
        
        public void AddNpcToActive(INpcController npcController)
        {
            ActiveNpcControllers.Add(npcController);
        }

        public void RemoveNpcFromActive(INpcController npcController)
        {
            ActiveNpcControllers.Remove(npcController);
        }
    }
}