using System;
using UnityEngine;
using static InputPCSystemControls;

namespace Zombieland.GameScene0.UIModule
{
    public class InputPC : MonoBehaviour
    {
        public event Action<Vector2> OnKeyboardMoved;
        public event Action<string> OnButtonClick;

        private InputPCSystemControls _inputPCSystemControls;
        private MainActions _inputAction;

        private void Awake()
        {
            _inputPCSystemControls = new InputPCSystemControls();
            _inputAction = _inputPCSystemControls.Main;
        }

        private void OnEnable()
        {
            _inputPCSystemControls.Enable();
            _inputAction.Move.performed += context => Move();
            _inputAction.Move.canceled += context => Move();
            //_inputAction.Fire.performed += context => ButtonClick(NamePCButton.Fire);
            _inputAction.Fire.performed += context =>
            {
                Debug.Log("Fire performed");
                ButtonClick(NamePCButton.Fire);
            };
        }
        private void OnDisable()
        {
            _inputAction.Move.performed -= context => Move();
            _inputAction.Move.canceled -= context => Move();
            _inputAction.Fire.performed -= context => ButtonClick(NamePCButton.Fire);
            _inputPCSystemControls.Disable();
        }

        private void Move()
        {
            Vector2 moveVector = _inputAction.Move.ReadValue<Vector2>();

            OnKeyboardMoved?.Invoke(moveVector);
        }

        private void ButtonClick(NamePCButton nameButton)
        {
            Debug.Log("ButtonClick - " + nameButton);
            OnButtonClick?.Invoke(nameButton.ToString());
        }
    }
}
