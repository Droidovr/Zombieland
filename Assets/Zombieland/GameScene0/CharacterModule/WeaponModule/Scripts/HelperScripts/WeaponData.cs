using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    [Serializable]
    public class WeaponData
    {
        [JsonIgnore] public ICharacterController Owner;

        public string ID;
        public string Name;
        public string PrefabName;
        public List<string> AvailableImpactIDs;
        public float ShootCooldown;
        public float ShotAccuracy;
        public int MaxImpactCount;
        public bool HasTarget;
    }
}