using System;
using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCAIController : Controller, INPCAIController
    {
        public event Action SlotNumber1;
        public event Action SlotNumber2;
        public event Action SlotNumber3;
        public event Action SlotNumber4;
        public event Action<bool> OnFire;

        public INPCController NPCController { get; private set; }
        public bool IsPatrolling { get; private set; }

        private NPCPatrolling _nPCPatrolling;
        private NPCDetect _nPCDetect;

        public NPCAIController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            NPCController = parentController as INPCController;
            IsPatrolling = true;
        }

        public override void Disable()
        {
            _nPCPatrolling.StopPatrolling();
            _nPCDetect.StopDestenation();

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            _nPCPatrolling = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCPatrolling>();
            _nPCPatrolling.Init(this);
            _nPCPatrolling.StartPatrolling();

            _nPCDetect = NPCController.NPCVisualBodyController.NPCInScene.AddComponent<NPCDetect>();
            _nPCDetect.Init(this);

            NPCController.NPCAwarenessController.OnDetectCharacter += DetectCharacterHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void DetectCharacterHandler(IController character, bool isDetect)
        {
            if (isDetect)
            {
                _nPCPatrolling.StopPatrolling();

                ICharacterController characterController = character as ICharacterController;
                if (characterController != null)
                {
                    _nPCDetect.StartDestenation(characterController.VisualBodyController.CharacterInScene.transform);
                }
            }
            else
            { 
                _nPCDetect?.StopDestenation();
                _nPCPatrolling.StartPatrolling();
            }
        }
    }
}