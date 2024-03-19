namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IShotProcess
    {
        IWeapon Weapon { get; set; }
        IConsumption Consumption { get; set; }
        float CheckFirePermissionPeriod { get; set; }
        float TimeBetweenShots { get; set; }
        float TimeBetweenRecharges { get; set; }


        void StartFire();
        void StopFire();
    }
}