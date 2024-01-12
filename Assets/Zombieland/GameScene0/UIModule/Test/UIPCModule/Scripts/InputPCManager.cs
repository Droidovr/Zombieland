using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InputPCManager : MonoBehaviour
    {
        public event Action<Vector2> OnKeyboardMoved;
        public event Action OnFire;

        private InputPCSystemControls _inputMobileSystemControls;

        private void Awake()
        {
            _inputMobileSystemControls = new InputPCSystemControls();
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
            Vector2 moveVector = _inputMobileSystemControls.Main.Move.ReadValue<Vector2>();

            OnKeyboardMoved?.Invoke(moveVector);
        }

        private void Fire()
        {
            OnFire?.Invoke();
        }
    }
}
