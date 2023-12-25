using Zombieland.GameDataModule;

namespace Zombieland.RootModule
{
    public interface IRootController
    {
        //TODO : Add required subsystems here
        IGameDataController GameDataController { get; }
    }
}