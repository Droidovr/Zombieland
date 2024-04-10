using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.NPCModule;
using Zombieland.GameScene0.RootModule;

namespace Zombieland.GameScene0.NPCManagerModule
{
    public class NPCManagerController : Controller, INPCManagerController
    {
        private IRootController _rootController;
        public Transform CharacterTransform { get; set; }

        private List<INPCController> _NPCControllers;

        public NPCManagerController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            //_rootController = (IRootController)parentController;
            //CharacterTransform = _rootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            CreateHelpersScripts();
            CreateNPC();
        }

        protected override void CreateHelpersScripts()
        {
            _NPCControllers = new List<INPCController>();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
        
        }

        private void CreateNPC()
        {
            var NPC = new NPCController(this, null);
            _NPCControllers.Add(NPC);
        }
    }
}