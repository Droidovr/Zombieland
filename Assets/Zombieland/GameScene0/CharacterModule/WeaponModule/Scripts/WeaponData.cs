using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponData
    {
        ICharacterController Owner;
        string ID;
        string Name;
        string PrefabName;
        Vector3 DeparturePoint; //localPosition вылета пули т.п.
        string FVXShotingName;
        List<string> AvailableImpactIDs; // это список разрешенных зарядов для стрельбы - для екипировки
    }
}