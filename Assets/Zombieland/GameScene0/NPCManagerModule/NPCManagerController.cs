using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NPCManagerController : Controller, INPCManagerController
    {
        public IRootController RootController { get; set; }
        public Transform CharacterTransform { get; set; }

        private List<INPCController> _activeNPCControllers;

        public NPCManagerController(IController parentController, List<IController> requiredControllers, Transform characterTransform) 
            : base(parentController, requiredControllers)
        {
            //_rootController = (IRootController)parentController;
            //CharacterTransform = _rootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            CharacterTransform = characterTransform;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _activeNPCControllers = new List<INPCController>();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _activeNPCControllers = new List<INPCController>();
            CreateNPC();
        }

        private void CreateNPC()
        {
            var NPCController = new NPCController(this, null);
            _activeNPCControllers.Add(NPCController);
        }
    }
}