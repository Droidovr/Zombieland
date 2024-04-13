using System;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public interface IEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action OnEquipmentChanged;
        event Action<string> OnImpactChanged;
        event Action OnAmmoDepleted;

        int CurrentImpactCount { get; set; }

        ICharacterController CharacterController { get; }

        void PickUpWeapon(Weapon weapon);

        void ReloadCurrentWeapon();
    }
}