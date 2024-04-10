using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public interface IEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnEquipmentChanged;
        event Action<string> OnAmmoChanged;
        event Action OnAmmoDepleted;

        Dictionary<int, Weapon> WeaponsSlots { get; }
        Dictionary<string, int> CurrentImpactsEquipped { get; }
        int CurrentImpactCount { get; }
        string CurrentOutfitEquipped { get; }

        ICharacterController CharacterController { get; }

    }
}