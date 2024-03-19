using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class WeaponData
    {
        public ICharacterController Owner;
        public string ID;
        public string Name;
        public string PrefabName;
        public Vector3 DeparturePoint; //localPosition вылета пули т.п.
        public string FVXShotingName;
        public List<string> AvailableImpactIDs; // это список разрешенных зарядов для стрельбы - для екипировки
    }
}