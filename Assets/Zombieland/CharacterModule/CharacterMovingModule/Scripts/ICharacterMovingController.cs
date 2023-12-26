using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        void Move(Vector2 direction);
    }
}