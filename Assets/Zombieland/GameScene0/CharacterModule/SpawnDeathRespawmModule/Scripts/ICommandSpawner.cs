using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule.Scripts;

namespace Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule
{
  public interface ICommandSpawner : ICommand
  {
    SpawnHelper SpawnHelper { get; set; }
  
  }
}