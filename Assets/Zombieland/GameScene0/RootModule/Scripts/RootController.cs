using System;
using System.Collections.Generic;
using Zombieland.GameScene0.CameraModule;
using Zombieland.GameScene0.CharacterModule;
using Zombieland.GameScene0.CharacterModule.BuffDebuffModule;
using Zombieland.GameScene0.EnvironmentModule;
using Zombieland.GameScene0.GameDataModule;
using Zombieland.GameScene0.UIModule;

namespace Zombieland.GameScene0.RootModule
{
    public class RootController : Controller, IRootController
    {
        public ICharacterController CharacterController { get; private set; }
        public IGameDataController GameDataController { get; private set; }
        public IEnvironmentController EnvironmentController { get; private set; }
        public IUIController UIController { get; private set; }
        public ICameraController CameraController { get; private set; }

        public RootController(IController parentController, List<IController> requiredControllers) : base(
            parentController, requiredControllers)
        {
            // This class’s constructor doesn’t have any content yet.
            OnReady += TestBuffDebuffSystem;
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            CharacterController = new CharacterController(this, new List<IController> {(IController) EnvironmentController, (IController) GameDataController, (IController) UIController});
            GameDataController = new GameDataController(this, null);
            EnvironmentController = new EnvironmentController(this, new List<IController> {(IController) GameDataController});
            UIController = new UIController(this, null);
            CameraController = new CameraController(this, new List<IController> {(IController)CharacterController});

            subsystemsControllers.Add((IController) CharacterController);
            subsystemsControllers.Add((IController) GameDataController);
            subsystemsControllers.Add((IController) EnvironmentController);
            subsystemsControllers.Add((IController) UIController);
            subsystemsControllers.Add((IController) CameraController);
        }

        private void TestBuffDebuffSystem(string errorMessage, IController controller)
        {
            Slowdown slowdown = new Slowdown();
            slowdown.Name = "Slowdown";
            DirectImpactSetting directImpactSettingSlowdown = new DirectImpactSetting();
            directImpactSettingSlowdown.Type = DirectImpactType.Poison;
            directImpactSettingSlowdown.PercentageValue = 50;
            slowdown.DirectImpactSetting = directImpactSettingSlowdown;
            slowdown.ImpactTarget = CharacterController;
            slowdown.Owner = CharacterController;
            slowdown.LifeTime = 30000;
            //CharacterController.BuffDebuffController.InjectDebuffs(new List<IBuffDebuffCommand> { slowdown });


            InfectedWound infectedWound = new InfectedWound();
            infectedWound.Name = "InfectedWound";
            DirectImpactSetting directImpactSettingInfectedWound = new DirectImpactSetting();
            directImpactSettingInfectedWound.Type = DirectImpactType.Poison;
            directImpactSettingInfectedWound.AbsoluteValue = 1;
            infectedWound.DirectImpactSetting = directImpactSettingInfectedWound;
            infectedWound.ImpactTarget = CharacterController;
            infectedWound.Owner = CharacterController;
            infectedWound.LifeTime = 20000;
            infectedWound.Interval = 1000;
            //CharacterController.BuffDebuffController.InjectDebuffs(new List<IBuffDebuffCommand> { infectedWound });

            WeakTreatment weakTreatment = new WeakTreatment();
            weakTreatment.Name = "WeakTreatment";
            DirectImpactSetting directImpactSettingWeakTreatment = new DirectImpactSetting();
            directImpactSettingWeakTreatment.Type = DirectImpactType.NotType;
            directImpactSettingWeakTreatment.AbsoluteValue = 2;
            weakTreatment.DirectImpactSetting = directImpactSettingInfectedWound;
            weakTreatment.ImpactTarget = CharacterController;
            weakTreatment.Owner = CharacterController;
            weakTreatment.LifeTime = 10000;
            weakTreatment.Interval = 3000;
            //CharacterController.BuffDebuffController.InjectDebuffs(new List<IBuffDebuffCommand> { weakTreatment });

            CharacterController.BuffDebuffController.InjectDebuffs(new List<IBuffDebuffCommand> { slowdown, infectedWound, weakTreatment });
        }
    }
}