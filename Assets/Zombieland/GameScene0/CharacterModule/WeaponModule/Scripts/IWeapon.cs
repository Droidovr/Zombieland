using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        ICharacterController Owner { get; set; }
        string ID { get; set; }
        string Name { get; set; }
        string PrefabName { get; set; }
        Vector3 DeparturePoint { get; set; }
        //List<Impact> AvailableImpactTypes { get; set; }
        IShotCommand ShotHandler { get; set; }
    }
}