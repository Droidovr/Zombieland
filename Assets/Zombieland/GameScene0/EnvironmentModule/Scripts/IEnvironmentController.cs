using System;


namespace Zombieland.GameScene0.EnvironmentModule
{
    public interface IEnvironmentController
    {
        event Action OnSceneLoaded;

        string CurrentLevelName { get; }
    }
}