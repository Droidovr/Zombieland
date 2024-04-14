using System.Collections.Generic;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class Pistol
    {
        private Weapon _pistol;
        private IWeaponController _weaponController;

        public Pistol(IWeaponController weaponController)
        {
            _pistol = new Weapon();
            _pistol.WeaponData = new WeaponData();
            _weaponController = weaponController;
        }

        public void Init()
        {
            _pistol.WeaponData.ID = "Pistol_0";
            _pistol.WeaponData.Name = "Pistol";
            _pistol.WeaponData.PrefabName = "Pistol_0";
            _pistol.WeaponData.AvailableImpactIDs = new List<string> { "GunBullet_0", "GunBullet_1" };
            _pistol.WeaponData.ShootCooldown = 1f;
            _pistol.WeaponData.ShotAccuracy = 0.5f;
            _pistol.WeaponData.MaxImpactCount = 15;
            _pistol.WeaponData.HasTarget = false;
        }

        public void Serialize() 
        {
            _weaponController.CharacterController.RootController.GameDataController.SaveDada<Weapon>("Pistol_0", _pistol);
        }
    }
}