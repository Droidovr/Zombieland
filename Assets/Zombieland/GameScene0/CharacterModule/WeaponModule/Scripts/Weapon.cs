using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class Weapon : IWeapon
    {
        public ICharacterController Owner { get; private set; }
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string PrefabName { get; private set; }
        public Vector3 DeparturePoint { get; private set; }
        public IShotCommand ShotHandler { get; private set; }
    }
}