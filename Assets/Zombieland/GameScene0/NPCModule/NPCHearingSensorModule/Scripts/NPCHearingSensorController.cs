using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts
{
    public class NPCHearingSensorController : Controller, INPCHearingSensorController
    {
        private readonly INPCController _NPCController;
        private HearingSensor _hearingSensor;

        public NPCHearingSensorController(IController parentController, List<IController> requiredControllers)
            : base(parentController, requiredControllers)
        {
            _NPCController = (INPCController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _hearingSensor = _NPCController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _hearingSensor = _NPCController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        private void OnCharacterInsideZone(bool isInsideZone)
        {
            
        }
    }
}
