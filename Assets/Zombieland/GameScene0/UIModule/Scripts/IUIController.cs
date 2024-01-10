namespace Zombieland.GameScene0.UIModule
{
    public interface IUIController
    {
        IUIMobileController UIMobileController { get; }
        IUIPCController UIPCController { get; }
    }
}
