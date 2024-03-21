using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class WeaponData
    {
        [JsonIgnore] public ICharacterController Owner;

        public string ID;
        public string Name;
        public string PrefabName;
        public Vector3 FirePoint;
        public string FVXShotingName;
        public List<string> AvailableImpactIDs;
        public float ShootCooldown;
        public float ReloadCooldown;
    }
}