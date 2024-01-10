using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InputMobileManager : MonoBehaviour
    {
        public event Action<Vector2> OnJoystickMoved;
        public event Action OnFire;

        private InputMobileSystemControls _inputMobileSystemControls;

        private void Awake()
        {
            _inputMobileSystemControls = new InputMobileSystemControls();            
        }

        private void OnEnable()
        {
            _inputMobileSystemControls.Enable();
            _inputMobileSystemControls.Main.Move.performed += context => Move();
            _inputMobileSystemControls.Main.Move.canceled += context => Move();
            _inputMobileSystemControls.Main.Fire.performed += context => Fire();
        }
        private void OnDisable()
        {
            _inputMobileSystemControls.Main.Move.performed -= context => Move();
            _inputMobileSystemControls.Main.Move.canceled -= context => Move();
            _inputMobileSystemControls.Main.Fire.performed -= context => Fire();
            _inputMobileSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputMobileSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnJoystickMoved?.Invoke(joistickPosition);
        }

        private void Fire()
        { 
            OnFire?.Invoke();
        }
    }
}
