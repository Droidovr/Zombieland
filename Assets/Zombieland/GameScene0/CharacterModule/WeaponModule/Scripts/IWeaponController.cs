using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        event Action OnAmmoDepleted;
        event Action OnShotAnimationPreparing;
        event Action OnShotPerformed;
        event Action OnShotFailed;

        ICharacterController CharacterController { get; }
        IWeapon Weapon { get; }
        string CurrentImpactName { get; }
        int CurrentImpactCount { get; set; }
    }
}