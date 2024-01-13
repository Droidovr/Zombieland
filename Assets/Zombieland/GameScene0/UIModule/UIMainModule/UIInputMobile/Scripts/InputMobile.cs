using System;
using UnityEngine;
using static InputMobileSystemControls;

namespace Zombieland.GameScene0.UIModule
{
    public class InputMobile : MonoBehaviour
    {
        public event Action<Vector2> OnJoystickMoved;
        public event Action<string> OnButtonClick;

        private InputMobileSystemControls _inputMobileSystemControls;
        private MainActions _inputAction;

        private void Awake()
        {
            _inputMobileSystemControls = new InputMobileSystemControls();
            _inputAction = _inputMobileSystemControls.Main;
        }

        private void OnEnable()
        {
            _inputMobileSystemControls.Enable();
            _inputAction.Move.performed += context => Move();
            _inputAction.Move.canceled += context => Move();
            _inputAction.Fire.performed += context => ButtonClick(NameMobileButton.Fire);
        }
        private void OnDisable()
        {
            _inputAction.Move.performed -= context => Move();
            _inputAction.Move.canceled -= context => Move();
            _inputAction.Fire.performed -= context => ButtonClick(NameMobileButton.Fire);
            _inputMobileSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputMobileSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnJoystickMoved?.Invoke(joistickPosition);
        }

        private void ButtonClick(NameMobileButton nameButton)
        {
            OnButtonClick?.Invoke(nameButton.ToString());
        }
    }
}
