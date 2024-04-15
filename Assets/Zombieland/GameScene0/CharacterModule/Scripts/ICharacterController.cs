using Zombieland.GameScene0.CharacterModule.CharacterMovingModule;
using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.InventoryModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeImpactModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.RootModule;
using Zombieland.GameScene0.VisualBodyModule;
using Zombieland.GameScene0.CharacterModule.AnimationModule;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.CharacterModule.AimingModule;
using Zombieland.GameScene0.CharacterModule.StealthModule;

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
        IInventoryController InventoryController { get; }
        IAnimationController AnimationController { get; }
        IBuffDebuffController BuffDebuffController { get; }
        IAimingController AimingController { get; }
        IStealthController StealthController { get; }

        Transform CharacterTransform { get; }
    }
}