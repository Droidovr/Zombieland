using System.Collections.Generic;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.VisualBodyModule
{
    public interface IVisualBodyController
    {
        GameObject CharacterInScene { get; }
        GameObject WeaponInScene { get; }
        Transform WeaponPointFire { get; }
        AudioSource WeaponSoundFire { get; }
        ParticleSystem WeaponVFX { get; }
        List<GameObject> SensorTriggersGameobject { get; }
        ICharacterController CharacterController { get; }
    }
}