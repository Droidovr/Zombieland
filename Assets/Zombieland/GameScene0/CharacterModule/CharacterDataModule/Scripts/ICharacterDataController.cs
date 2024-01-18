namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public interface ICharacterDataController
    {
        float MaxMovingSpeed { get; }
        float MaxRotationSpeed { get; }
        float DesignMovingSpeed { get; set; }
        float DesignRotationSpeed { get; set; }
        float Gravity { get; }
        ICharacterController CharacterController { get; }
    }
}