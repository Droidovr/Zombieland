using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class Weapon : IWeapon
    {
        public WeaponData WeaponData { get; set; }
        public IShotProcess ShotProcess { get; private set; }

        public void Init(IWeaponController weaponController)
        {
            WeaponData.Owner = weaponController.CharacterController;
            ShotProcess = weaponController.CharacterController.VisualBodyController.WeaponInScene.GetComponent<ShotProcess>();
            ShotProcess.Init(weaponController);
        }
    }
}