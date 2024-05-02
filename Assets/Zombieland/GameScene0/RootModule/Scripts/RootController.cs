using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CameraModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;
using Zombieland.GameScene0.NPCManagerModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.RootModule
{
    public class RootController : Controller, IRootController
    {
        public ICharacterController CharacterController { get; private set; }
        public IGameDataController GameDataController { get; private set; }
        public IEnvironmentController EnvironmentController { get; private set; }
        public INpcManagerController NpcManagerController { get; private set; }
        public IUIController UIController { get; private set; }
        public ICameraController CameraController { get; private set; }

        public RootController(IController parentController, List<IController> requiredControllers) 
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
            GameDataController = new GameDataController(this, null);
            EnvironmentController = new EnvironmentController(this, new List<IController> {(IController) GameDataController});
            CharacterController = new CharacterController(this, new List<IController> {(IController)GameDataController, (IController)EnvironmentController, (IController)UIController});
            NpcManagerController = new NpcManagerController(this, new List<IController>{(IController)GameDataController, (IController)EnvironmentController, (IController)CharacterController});
            UIController = new UIController(this, null);
            CameraController = new CameraController(this, new List<IController> {(IController)CharacterController});

            subsystemsControllers.Add((IController) GameDataController);
            subsystemsControllers.Add((IController) EnvironmentController);
            subsystemsControllers.Add((IController) CharacterController);
            subsystemsControllers.Add((IController) NpcManagerController);
            subsystemsControllers.Add((IController) UIController);
            subsystemsControllers.Add((IController) CameraController);
        }
    }
}