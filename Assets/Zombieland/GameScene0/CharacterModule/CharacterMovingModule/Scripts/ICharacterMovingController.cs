namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public interface ICharacterMovingController
    {
        float RealMovingSpeed { get; }
        ICharacterController CharacterController { get; }
    }
}