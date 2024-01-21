using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeImpactModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
        ISensorController SensorController { get; }
        ITakeImpactController TakeImpactController { get; }
        IEquipmentController EquipmentController { get; }
    }
}