using System;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule.RobotDeadBodySensorModule;


namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotAwarenesBodyModule
{
    public interface IRobotAwarenesController
    {
        IRobotController RobotController { get; }
        IRobotDeadBodySensorController RobotDeadBodySensorController { get; }
    }
}