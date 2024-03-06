namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        float RealMovingSpeed { get; set; }
        ICharacterController CharacterController { get; }
        void ActivateMoving(bool isActive);
    }
}