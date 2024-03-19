using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        event Action OnAmmoDepleted;
        event Action OnShotPerformed;
        event Action OnShotFailed;

        string CurrentImpactName { get; }
        Dictionary<string, TestImpact> Impacts { get; }
    }
}