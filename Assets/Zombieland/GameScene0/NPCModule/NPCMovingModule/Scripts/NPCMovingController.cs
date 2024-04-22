using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public class NPCMovingController : Controller, INPCMovingController
    {
        private readonly INPCController _NPCController;
        private NavMeshHandler _navMeshHandler;

        public NPCMovingController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INPCController)parentController;
            TestCreateSubsystem();
        }

        protected override void CreateHelpersScripts()
        {
            _navMeshHandler = _NPCController.VisualBodyController.ActiveNPC.GetComponent<NavMeshHandler>();
            _navMeshHandler.Init(_NPCController.DataController.NPCData.speed);
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            //This method has no implementation
        }

        public void MoveToTarget(Vector3 targetPosition, float stoppingDistance, Action onTargetReached = null)
        {
            _navMeshHandler.MoveToTarget(targetPosition, stoppingDistance, onTargetReached);
        }

        public void FollowTarget(Transform targetTransform, float stoppingDistance, Action onTargetReached = null)
        {
            _navMeshHandler.FollowTarget(targetTransform, stoppingDistance, onTargetReached);
        }

        public void StopMoving()
        {
            _navMeshHandler.StopMoving();
        }
        
        public bool IsMoving()
        {
            return _navMeshHandler.IsMoving;
        }

        private void TestCreateSubsystem()
        {
            _navMeshHandler = _NPCController.VisualBodyController.ActiveNPC.GetComponent<NavMeshHandler>();
            _navMeshHandler.Init(_NPCController.DataController.NPCData.speed);
        }
    }
}
