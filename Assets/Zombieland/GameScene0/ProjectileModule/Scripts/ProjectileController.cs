using System.Collections.Generic;

namespace Zombieland.GameScene0.ProjectileModule
{
    public class ProjectileController : Controller, IProjectileController
    {
        public float Speed { get; set; }
        public int Damage { get; set; }
        
        public ProjectileController(IController parentController, List<IController> requiredControllers) 
            : base(parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }
    }
}
