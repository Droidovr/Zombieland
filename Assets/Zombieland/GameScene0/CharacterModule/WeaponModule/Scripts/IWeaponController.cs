﻿using System;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeaponController
    {
        event Action OnAmmoDepleted;
        event Action OnShotPerformed;
        event Action OnShotFailed;
    }
}