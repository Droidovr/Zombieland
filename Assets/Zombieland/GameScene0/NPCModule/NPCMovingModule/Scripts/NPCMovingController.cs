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
            CreateHelpersScripts();
        }

        protected override void CreateHelpersScripts()
        {
            _navMeshHandler = _NPCController.VisualBodyController.ActiveNPC.GetComponent<NavMeshHandler>();
            _navMeshHandler.Init(_NPCController.DataController.NPCData.Speed);
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
    }
}
