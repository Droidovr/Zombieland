using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCSpawnModule
{
    public class NpcSpawnController : Controller, INpcSpawnController
    {
        private readonly INpcController _npcController;

        public NpcSpawnController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _npcController = (INpcController)parentController;
        }

        protected override void CreateHelpersScripts()
        {
            Debug.Log(")))))))))))))))))))) - NpcSpawnController");
            SetAndActivateNPC();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        private void SetAndActivateNPC()
        {
            Debug.Log("***********************" + _npcController.NpcVisualBodyController.NpcOnScene.name);
            Debug.Log("***********************" + _npcController.NpcDataController.NpcData);
            Debug.Log("***********************" + _npcController.NpcDataController.NpcData.SpawnData);
            Debug.Log("***********************" + _npcController.NpcDataController.NpcData.SpawnData.SpawnPositionTransform);



            _npcController.NpcVisualBodyController.NpcOnScene.transform.position = _npcController.NpcDataController.NpcData.SpawnData.SpawnPositionTransform.position;
            _npcController.NpcVisualBodyController.NpcOnScene.SetActive(true);
        }
    }
}
