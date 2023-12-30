using System;
using UnityEngine;
using Zombieland.RootModule;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterMovingController : IController, ICharacterMovingController
    {
        public bool IsActive { get; private set; }
        public Vector2 DirectionMove
        {
            get
            {
                return _testCharacterController.TestUIController.GetVectorInput(); ;
            }
        }


        public event Action<string, IController> OnReady;


        private ITestCharacterController _testCharacterController;
        private IController _testVisualBodyController;
        private IController _testCharacterDataController;
        private IController _testUIController;
        private GameObject _character;
        private TestCharacterData _testCharacterData;
        private Rigidbody _characterRigidbody;

        private bool _visualBodyControllerReady;
        private bool _characterDataControllerReady;
        private bool _uiControllerReady;

        public void Disable()
        {
            SetSystemsActivity(false);
        }

        public void Initialize<T>(T parentController)
        {
            _testCharacterController = parentController as ITestCharacterController;
            _testVisualBodyController = _testCharacterController.TestVisualBodyController as IController;
            _testCharacterDataController = _testCharacterController.TestCharacterDataController as IController;
            _testUIController = _testCharacterController.TestUIController as IController;

            _testVisualBodyController.OnReady += PhysicsInitializer;
            _testCharacterDataController.OnReady += SetPhysicCharacterProperties;
            _testUIController.OnReady += SetInput;
        }

        private void PhysicsInitializer(string arg1, IController controller)
        {
            _character = _testCharacterController.TestVisualBodyController.GetCharacterGameobject();

            _character.AddComponent<Rigidbody>();
            _character.AddComponent<CapsuleCollider>();
            
            _visualBodyControllerReady = true;
            OnControllerReady();
        }

        private void SetPhysicCharacterProperties(string arg1, IController controller)
        {
            if (_character != null)
            {
                _testCharacterData = _testCharacterController.TestCharacterDataController.GetCharacterData();
                _characterRigidbody = _character.GetComponent<Rigidbody>();

                _characterRigidbody.mass = _testCharacterData.Mass;
                _characterRigidbody.drag = _testCharacterData.Drag;
                _characterRigidbody.angularDrag = _testCharacterData.AngularDrag;
                _characterRigidbody.useGravity = _testCharacterData.UseGravity;
                _characterRigidbody.isKinematic = _testCharacterData.IsKinematic;

                RigidbodyConstraints constraints = RigidbodyConstraints.None;
 
                if (_testCharacterData.ConstaintsFreezePositionX)
                    constraints |= RigidbodyConstraints.FreezePositionX;
                if (_testCharacterData.ConstaintsFreezePositionY)
                    constraints |= RigidbodyConstraints.FreezePositionY;
                if (_testCharacterData.ConstaintsFreezePositionZ)
                    constraints |= RigidbodyConstraints.FreezePositionZ;
                if (_testCharacterData.ConstaintsFreezeRotationX)
                    constraints |= RigidbodyConstraints.FreezeRotationX;
                if (_testCharacterData.ConstaintsFreezeRotationY)
                    constraints |= RigidbodyConstraints.FreezeRotationY;
                if (_testCharacterData.ConstaintsFreezeRotationZ)
                    constraints |= RigidbodyConstraints.FreezeRotationZ;

                _characterRigidbody.constraints = constraints;

                _characterDataControllerReady = true;
                OnControllerReady();
            }
        }

        private void SetInput(string arg1, IController controller)
        {
            _uiControllerReady = true;
            OnControllerReady();
        }

        private void OnControllerReady()
        {
            if (_visualBodyControllerReady && _characterDataControllerReady && _uiControllerReady)
            {
                _character.AddComponent<CharacterPhysicMoving>();

                CharacterPhysicMoving characterPhysicMoving = _character.GetComponent<CharacterPhysicMoving>();

                characterPhysicMoving.MovingSpeed = _testCharacterData.SpeedMoving;
                characterPhysicMoving.MovingRotation = _testCharacterData.SpeedRotation;
                characterPhysicMoving.CharacterMovingController = this;


                SetSystemsActivity(true);
            }
        }

        private void SetSystemsActivity(bool isActive)
        {
            IsActive = isActive;
            OnReady?.Invoke(String.Empty, this);
        }
    }
}
