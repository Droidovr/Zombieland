using System.Collections.Generic;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.EquipmentModule
{
    public struct WeaponSlot
    {
        public Weapon EquippedWeapon;
        public Dictionary<string, int> EquippedImpacts;

        static readonly WeaponSlot emptySlot = new WeaponSlot(null, null);

        public WeaponSlot(Weapon equippedWeapon, Dictionary<string, int> equippedImpacts)
        {
            EquippedWeapon = equippedWeapon;
            EquippedImpacts = equippedImpacts;
        }

        public static WeaponSlot empty { get { return emptySlot; } }

        public void AddEquippedImpact(string impactID, int amount)
        {
            EquippedImpacts.Add(impactID, amount);
        }

        public void SetEquippedWeapon(Weapon equippedWeapon)
        {
            EquippedWeapon = equippedWeapon;
        }

        public static bool operator==(WeaponSlot a, WeaponSlot b)
        {
            if (a.EquippedWeapon == b.EquippedWeapon && a.EquippedImpacts == b.EquippedImpacts)
            {
                return true;
            }
            return false;
        }

        public static bool operator!=(WeaponSlot a, WeaponSlot b)
        {
            return !(a == b);
        }
    }
}