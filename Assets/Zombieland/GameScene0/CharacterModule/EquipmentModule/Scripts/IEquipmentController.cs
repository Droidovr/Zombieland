using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public interface IEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnEquipmentChanged;
        event Action<string> OnImpactChanged;
        event Action OnImpactDepleted;

        Dictionary<int, WeaponSlot> WeaponsSlots { get; }
        Dictionary<string, int> CurrentImpactsEquipped { get; }
        int CurrentImpactCount { get; set;  }
        string CurrentOutfitEquipped { get; }

        ICharacterController CharacterController { get; }

        void ReloadCurrentWeapon();
    }
}