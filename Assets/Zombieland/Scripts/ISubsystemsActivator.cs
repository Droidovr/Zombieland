using System.Collections.Generic;

namespace Zombieland
{
    public interface ISubsystemsActivator
    {
        bool IsReady { get; }
        List<IController> SubsystemsControllers { get; set; }
        void SetSubsystemsActivity(bool isActive);
    }
}