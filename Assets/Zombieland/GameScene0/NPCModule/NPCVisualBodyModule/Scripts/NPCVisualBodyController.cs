using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCVisualBodyController : Controller, INPCVisualBodyController
    {
        public GameObject NPCInScene { get; private set; }
        public List<GameObject> SensorTriggersGameobject { get; private set; }
        public INPCController NPCController { get; private set; }

        private CreateNPCPrefab _createNPCGameobject;

        public NPCVisualBodyController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;

            _createNPCGameobject = new CreateNPCPrefab();
        }

        protected override void CreateHelpersScripts()
        {
            CreateNPCGameobject();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn�t have any subsystems at the moment.
        }

        private void CreateNPCGameobject()
        {
            NPCInScene = _createNPCGameobject.CreateNPC(Vector3.zero, Quaternion.identity);
            NPCInScene.SetActive(false);
        }
    }
}