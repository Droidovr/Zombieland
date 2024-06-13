using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;
using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotVisualBodyModule;

namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public interface IRobotController
    {
        IRobotsManagerController RobotsManagerController { get; }
        RobotSpawnData RobotSpawnData { get; }
        IRobotDataController RobotDataController { get; }
        IRobotVisualBodyController RobotVisualBodyController { get; }
    }
}