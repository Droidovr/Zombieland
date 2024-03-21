using System;
using System.Collections.Generic;
using Zombieland.GameScene0.ImpactModule;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        event Action OnAmmoDepleted;
        event Action OnShotPerformed;
        event Action OnShotFailed;

        ICharacterController CharacterController { get; }
        string CurrentImpactName { get; }
        Dictionary<string, IImpact> Impacts { get; }
    }
}