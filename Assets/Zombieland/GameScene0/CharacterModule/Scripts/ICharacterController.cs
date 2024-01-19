using Zombieland.GameScene0.CharacterModule.CharacterDataModule;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.CharacterModule.SensorModule;
using Zombieland.GameScene0.CharacterModule.TakeDamageModule;
using Zombieland.GameScene0.CharacterModule.WeaponModule;
using Zombieland.GameScene0.ProjectileModule;
using Zombieland.GameScene0.VisualBodyModule;

namespace Zombieland.GameScene0.CharacterModule
{
    public interface ICharacterController
    {
        ICharacterDataController CharacterDataController { get; }
        IWeaponController WeaponController { get; }       
        IVisualBodyController VisualBodyController { get; }
        ISensorController SensorController { get; }
        ITakeDamageController TakeDamageController { get; }
        IEquipmentController EquipmentController { get; }

        public void TakeDamage(IProjectileController projectileController);
    }
}