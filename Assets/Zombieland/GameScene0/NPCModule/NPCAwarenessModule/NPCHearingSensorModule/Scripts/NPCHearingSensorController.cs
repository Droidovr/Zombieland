using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.StealthModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;

namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts
{
    public class NPCHearingSensorController : Controller, INPCHearingSensorController
    {
        private readonly INPCAwarenessController _INPCAwarenessController;
        private IStealthController _characterStealthController;
        private HearingSensor _hearingSensor;

        public NPCHearingSensorController(IController parentController, List<IController> requiredControllers)
            : base(parentController, requiredControllers)
        {
            _INPCAwarenessController = (INPCAwarenessController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _characterStealthController = _INPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController;
            _hearingSensor = _INPCAwarenessController.NPCController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_INPCAwarenessController.NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _hearingSensor = _INPCAwarenessController.NPCController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_INPCAwarenessController.NPCController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        private void OnCharacterInsideZone(bool isInsideZone)
        {
            if (isInsideZone)
            {
                if (_characterStealthController.IsStealth)
                {
                    _characterStealthController.OnStealth += OnStealthStateChange;
                }
                else
                {
                    NotifyAwarenessSystem(true);
                }
            }
            else
            {
                NotifyAwarenessSystem(false);
                _characterStealthController.OnStealth -= OnStealthStateChange;
            }
        }

        private void OnStealthStateChange(bool isStealth)
        {
            NotifyAwarenessSystem(!isStealth);
        }

        private void NotifyAwarenessSystem(bool isInsideZone)
        {
            _INPCAwarenessController.CanHearTarget(isInsideZone);
        }
    }
}
