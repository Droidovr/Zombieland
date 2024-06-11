using Zombieland.GameScene0.RobotsManagerModule.RobotModule.RobotDataModule;

namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public interface IRobotController
    {
        IRobotsManagerController RobotsManagerController { get; }
        RobotSpawnData RobotSpawnData { get; }
        IRobotDataController RobotDataController { get; }
    }
}