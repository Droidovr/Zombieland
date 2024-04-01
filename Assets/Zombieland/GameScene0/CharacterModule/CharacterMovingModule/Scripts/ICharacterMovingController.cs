using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        float RealMovingSpeed { get; set; }
        Vector3 DirectionWalk { get; set; }
        ICharacterController CharacterController { get; }
        void ActivateMoving(bool isActive);
    }
}