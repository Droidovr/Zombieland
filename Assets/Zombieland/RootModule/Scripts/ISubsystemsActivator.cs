using System;

namespace Zombieland.RootModule
{
    public interface ISubsystemsActivator
    {
        event Action<string> OnReady;
        void SetSubsystemsActivity(bool isActive);
    }
}