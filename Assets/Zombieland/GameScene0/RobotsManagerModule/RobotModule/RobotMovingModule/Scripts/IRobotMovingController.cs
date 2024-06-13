using System;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotMovingModule
{
    public interface IRobotMovingController
    {
        event Action<float, bool> OnMoving;

        IRobotController RobotController { get; }
        void ActivateMoving(bool isActive);
    }
}