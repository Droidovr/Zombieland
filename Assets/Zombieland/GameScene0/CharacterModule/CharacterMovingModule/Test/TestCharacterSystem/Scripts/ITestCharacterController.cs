namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public interface ITestCharacterController
    {
        ICharacterMovingController CharacterMovingController { get; }
        ITestVisualBodyController TestVisualBodyController { get; }
        ITestCharacterDataController TestCharacterDataController { get; }
        ITestUIController TestUIController { get; }

        //TODO : Add Properties or Method other System
    }
}
