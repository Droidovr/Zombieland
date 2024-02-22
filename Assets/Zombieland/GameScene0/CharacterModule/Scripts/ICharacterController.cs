using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
﻿using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeImpactModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;
using Zombieland.GameScene0.CharacterModule.AnimationModule;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.SpawnDeathRespawmModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        IRootController RootController { get; }
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
        ICharacterMovingController CharacterMovingController { get; }
        ISensorController SensorController { get; }
        ITakeImpactController TakeImpactController { get; }
        IEquipmentController EquipmentController { get; }
        IAnimationController AnimationController { get; }

        ISpawnDeathRespawnController SpawnDeathRespawnController { get; }

        Transform CharacterTransform { get; }
    }
}