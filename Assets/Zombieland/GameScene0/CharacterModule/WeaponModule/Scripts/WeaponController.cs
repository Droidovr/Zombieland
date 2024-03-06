using System;
using System.Collections.Generic;


namespace Zombieland.GameScene0.CharacterModule.WeaponModule
{
    public class WeaponController : Controller, IWeaponController
    {
        public event Action OnAmmoDepleted;
        public event Action OnShotPerformed;
        public event Action OnShotFailed;

        public readonly ICharacterController CharacterController;

        public string CurrentImpactName;
        //public Dictionary<string, Impact> Impacts { get; set; }

        private Weapon _weapon;


        public WeaponController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            //Impacts = new Dictionary<string, Impact>();

            CharacterController = parentController as ICharacterController;
        }

        public override void Enable()
        {
            CharacterController.EquipmentController.OnWeaponChanged += SetWeapon;
            CharacterController.EquipmentController.OnAmmoChanged += OnAmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown += OnButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp += OnButtonFireUpHandler;

            base.Enable();
        }

        public override void Disable()
        {
            CharacterController.EquipmentController.OnWeaponChanged -= SetWeapon;
            CharacterController.EquipmentController.OnAmmoChanged -= OnAmmoChangedHandler;
            CharacterController.RootController.UIController.OnFireDown -= OnButtonFireDownHandler;
            CharacterController.RootController.UIController.OnFireUp -= OnButtonFireUpHandler;

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

        private void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }

        private void OnAmmoChangedHandler(string impactName)
        {
            // Subscribe to OnAmmoChanged in EquipmentSystem

            //if (!Impacts.ContainsKey(impactName))
            //{
            //    Impact impact = ДесериализуемИмпакт(impactName);
            //    Impacts.Add(impactName, impact);
            //}

            CurrentImpactName = impactName;
        }

        private void OnButtonFireDownHandler()
        {
            // Start Fire attempts
            _weapon.ShotHandler.Execute();
        }

        private void OnButtonFireUpHandler() 
        {
            // Stop Fire attempts
            _weapon.ShotHandler.Stop();
        }
    }
}