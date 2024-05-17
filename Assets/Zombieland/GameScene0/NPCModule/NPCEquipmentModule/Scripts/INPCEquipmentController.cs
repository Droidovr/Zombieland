using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.EquipmentModule;
using Zombieland.GameScene0.WeaponModule;


namespace Zombieland.GameScene0.NPCModule.NPCEquipmentModule
{
    public interface INPCEquipmentController
    {
        event Action<Weapon> OnWeaponChanged;
        event Action<string> OnEquipmentChanged;
        event Action OnImpactDepleted;

        List<WeaponSlot> WeaponSlots { get; }
        string CurrentImpactID { get; }
        int CurrentImpactCount { get; set; }
        string CurrentOutfitEquipped { get; }
        INPCController NPCController { get; }
    }
}