using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public interface IWeapon
    {
        ICharacterController Owner { get; }
        string ID { get; }
        string Name { get; }
        string PrefabName { get; }
        Vector3 DeparturePoint { get; }
        //List<Impact> AvailableImpactTypes { get; }
        IShotCommand ShotHandler { get; }
    }
}