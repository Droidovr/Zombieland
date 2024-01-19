using System.Collections.Generic;
using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.CharacterDataModule
{
    public class CharacterDataController : Controller, ICharacterDataController
    {
        public float MaxMovingSpeed { get; private set; }
        public float MaxRotationSpeed { get; private set; }
        public float Gravity { get; private set; }
        public float DesignMovingSpeed {
            get { return _designMovingSpeed; }
            set { _designMovingSpeed = value < 0 ? 0 : value; }
        }
        public float DesignRotationSpeed 
        {
            get { return _designRotationSpeed; }
            set { _designRotationSpeed = value < 0 ? 0 : value; }
        }
        public ICharacterController CharacterController { get; private set; }

        private float _designMovingSpeed;
        private float _designRotationSpeed;

        public CharacterDataController(IController parentController, List<IController> requiredControllers) : base(parentController, requiredControllers)
        {
            CharacterController = parentController as ICharacterController;
        }

        protected override void CreateHelpersScripts()
        {
            LoadDefaultValue();
        }

        protected override void CreateSubsystems(ref List<IController> subsystemsControllers)
        {
            // This controller doesn’t have any subsystems at the moment.
        }

        private void LoadDefaultValue()
        {
            CharacterData characterData = CharacterController.RootController.GameDataController.GetData<CharacterData>("CharacterData");
            MaxMovingSpeed = characterData.MaxMovingSpeed;
            DesignMovingSpeed = MaxMovingSpeed;
            MaxRotationSpeed = characterData.MaxRotationSpeed;
            DesignRotationSpeed = MaxRotationSpeed;
            Gravity = characterData.Gravity;
        }
    }
}