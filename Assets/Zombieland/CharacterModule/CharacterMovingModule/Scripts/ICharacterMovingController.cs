using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        Vector2 DirectionMove { get; set; }
    }
}