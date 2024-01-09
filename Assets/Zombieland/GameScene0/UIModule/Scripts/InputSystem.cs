using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InputSystem : MonoBehaviour
    {
        public event Action<Vector2> OnJoystickMoved;

        private InputSystemControls _inputSystemControls;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
        }
        private void OnDisable()
        {
            _inputSystemControls.Disable();
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnJoystickMoved?.Invoke(joistickPosition);
        }
    }
}
