using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public string CurrentImpactName { get; private set; }
        public Dictionary<string, TestImpact> Impacts { get; private set; }
        public ICharacterController CharacterController { get; private set; }

        private Weapon _weapon;


        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            Impacts = new Dictionary<string, TestImpact>();

            CharacterController = parentController as ICharacterController;
        }

        public override void Enable()
        {
            CharacterController.EquipmentController.OnWeaponChanged += WeaponChangedHandler;
            CharacterController.EquipmentController.OnAmmoChanged += AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown += ButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp += ButtonFireUpHandler;

            base.Enable();
        }

        public override void Disable()
        {
            CharacterController.EquipmentController.OnWeaponChanged -= WeaponChangedHandler;
            CharacterController.EquipmentController.OnAmmoChanged -= AmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown -= ButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp -= ButtonFireUpHandler;

            base.Disable();
        }

        protected override void CreateHelpersScripts()
        {
            // This controller doesn’t have any helpers scripts at the moment.
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void WeaponChangedHandler(Weapon weapon)
        {
            _weapon = weapon;
        }

        private void AmmoChangedHandler(string impactName)
        {
            // Subscribe to OnAmmoChanged in EquipmentSystem

            if (!Impacts.ContainsKey(impactName))
            {
                //Impact impact = ДесериализуемИмпакт(impactName);
                TestImpact impact = new TestImpact();
                Impacts.Add(impactName, impact);
            }

            CurrentImpactName = impactName;
        }

        private void ButtonFireDownHandler()
        {
            _weapon.ShotProcess.StartFire();
        }

        private void ButtonFireUpHandler() 
        {
            _weapon.ShotProcess.StopFire();
        }
    }
}