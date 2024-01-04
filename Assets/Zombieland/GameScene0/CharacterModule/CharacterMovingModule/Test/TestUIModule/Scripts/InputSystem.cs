using System;
using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class InputSystem : MonoBehaviour
    {
        public event Action<Vector2> OnJoustickMoved;

        private InputSystemControls _inputSystemControls;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
        }

        private void OnEnable() => _inputSystemControls.Enable();
        private void OnDisable() => _inputSystemControls.Disable();
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnJoustickMoved?.Invoke(joistickPosition);
        }
    }
}
