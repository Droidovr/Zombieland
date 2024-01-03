using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.GameDataModule;

namespace Zombieland.GameScene0.RootModule
{
    public interface IRootController
    {
        //TODO : Add required subsystems here
        ICharacterController CharacterController { get; }
        IGameDataController GameDataController { get; }
    }
}