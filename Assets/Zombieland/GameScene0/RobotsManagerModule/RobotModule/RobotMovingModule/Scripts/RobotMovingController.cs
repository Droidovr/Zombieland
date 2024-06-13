using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotMovingModule
{
    public class RobotMovingController : Controller, IRobotMovingController
    {
        public event Action<float, bool> OnMoving;

        public IRobotController RobotController { get; private set; }

        private IRobotPhysicMoving _robotPhysicMoving;


        public RobotMovingController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            RobotController = parentController as IRobotController;
        }

        public void ActivateMoving(bool isActive)
        {
            _robotPhysicMoving.ActivateMoving(isActive);
        }


        protected override void CreateHelpersScripts()
        {
#if UNITY_STANDALONE// || UNITY_EDITOR
            _robotPhysicMoving = RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotPhysicMovingPC>();
#else
            _robotPhysicMoving = RobotController.RobotVisualBodyController.RobotInScene.AddComponent<RobotPhysicMovingMobile>();
#endif

            _robotPhysicMoving.Init(this);
            _robotPhysicMoving.OnMoving += MovingHandler;
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void MovingHandler(float speed, bool isMove)
        {
            OnMoving?.Invoke(speed, isMove);
        }
    }
}