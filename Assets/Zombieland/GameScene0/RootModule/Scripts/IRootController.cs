﻿using Zombieland.GameScene0.CameraModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.RootModule
{
    public interface IRootController
    {
        //TODO : Add required subsystems here
        ICharacterController CharacterController { get; }
        IGameDataController GameDataController { get; }
        IEnvironmentController EnvironmentController { get; }
        IUIController UIController { get; }
        ICameraController CameraController { get; }
    }
}