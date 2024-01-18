namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        ICharacterController CharacterController { get; }
        float RealMovingSpeed { get; }
    }
}