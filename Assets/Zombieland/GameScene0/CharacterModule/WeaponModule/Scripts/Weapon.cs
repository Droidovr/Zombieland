using System.Collections.Generic;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class Weapon : IWeapon
    {
        public ICharacterController Owner { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string PrefabName { get; set; }
        public Vector3 DeparturePoint { get; set; }
        //public List<Impact> AvailableImpactTypes { get; set; }
        public IShotCommand ShotHandler { get; set; }
    }
}