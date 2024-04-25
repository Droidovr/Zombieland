using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.StealthModule;
using Zombieland.GameScene0.NPCModule.NPCAwarenessModule;

namespace Zombieland.GameScene0.NPCModule.NPCHearingSensorModule.Scripts
{
    public class NpcHearingSensorController : Controller, INpcHearingSensorController
    {
        private readonly INpcAwarenessController _INPCAwarenessController;
        private IStealthController _characterStealthController;
        private HearingSensor _hearingSensor;

        public NpcHearingSensorController(IController parentController, List<IController> requiredControllers)
            : base(parentController, requiredControllers)
        {
            _INPCAwarenessController = (INpcAwarenessController) parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _characterStealthController = _INPCAwarenessController.NpcController.NPCManagerController.RootController.CharacterController.StealthController;
            _hearingSensor = _INPCAwarenessController.NpcController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_INPCAwarenessController.NpcController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }
        
        private void TestCreateSubsystem()
        {
            _hearingSensor = _INPCAwarenessController.NpcController.VisualBodyController.ActiveNPC.GetComponent<HearingSensor>();
            _hearingSensor.Init(_INPCAwarenessController.NpcController.NPCManagerController.CharacterTransform, OnCharacterInsideZone);
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
