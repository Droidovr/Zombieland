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
        public Transform CharacterTransform { get; private set; }
        public List<INpcController> ActiveNpcControllers { get; private set; }
        
        public NpcManagerController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            RootController = (IRootController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            CharacterTransform = RootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            ActiveNpcControllers = new List<INpcController>();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            var npcSearch = new GameObject("NpcSpawnSearch").AddComponent<NpcSpawnSearch>();
            var npcSpawnDataList = npcSearch.GetNpcSpawnDataList();
            
            Debug.Log("----------- OBJECT SEARCH -----------");
            foreach (var npcSpawnData in npcSpawnDataList)
            {
                INpcController npcController = new NpcController(this, null, npcSpawnData);
                subsystemsControllers.Add((IController) npcController);
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