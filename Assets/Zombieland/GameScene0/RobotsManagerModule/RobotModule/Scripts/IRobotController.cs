namespace Zombieland.GameScene0.RobotsManagerModule.RobotModule
{
    public interface IRobotController
    {
        IRobotsManagerController RobotsManagerController { get; }
        RobotSpawnData RobotSpawnData { get; }
    }
}