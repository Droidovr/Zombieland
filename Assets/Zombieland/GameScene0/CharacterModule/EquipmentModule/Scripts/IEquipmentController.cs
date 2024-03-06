using System;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public interface IEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnAmmoChanged;
    }
}