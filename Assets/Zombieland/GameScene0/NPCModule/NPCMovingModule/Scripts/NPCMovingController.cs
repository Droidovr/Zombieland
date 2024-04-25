using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.NPCModule.NPCMovingModule
{
    public class NpcMovingController : Controller, INpcMovingController
    {
        public bool IsMoving => _navMeshHandler.IsMoving;

        private readonly INpcController _NPCController;
        private NavMeshHandler _navMeshHandler;

        public NpcMovingController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            _NPCController = (INpcController)parentController;
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

        private void TestCreateSubsystem()
        {
            _navMeshHandler = _NPCController.VisualBodyController.ActiveNPC.GetComponent<NavMeshHandler>();
            _navMeshHandler.Init(_NPCController.DataController.NPCData.speed);
        }
    }
}
