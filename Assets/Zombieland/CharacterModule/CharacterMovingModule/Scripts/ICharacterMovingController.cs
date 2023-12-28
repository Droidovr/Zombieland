using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        Vector2 DirectionMove { get; set; }
    }
}