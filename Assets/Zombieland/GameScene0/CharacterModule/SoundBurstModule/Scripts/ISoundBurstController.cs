using UnityEngine;
using Zombieland.GameScene0.CharacterModule.WeaponModule;

namespace Zombieland.GameScene0.CharacterModule.SoundBurstModule.Scripts
{
    public interface ISoundBurstController
    {
        ICharacterController CharacterController { get; }

        void PlaySound(Weapon weapon);
    }
}
