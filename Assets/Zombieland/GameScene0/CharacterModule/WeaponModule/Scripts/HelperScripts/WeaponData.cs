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
        public string FVXShotingName;
        public string AnimationPreparing;
        public float TimeAnimationPreparing;
        public string AnimationShot;
        public Vector3 FirePoint;
        public List<string> AvailableImpactIDs;
        public float ShootCooldown;
        public float ReloadCooldown;
        public float ShotAccuracy;
        public int MaxImpactCount;
        public bool HasTarget;
    }
}