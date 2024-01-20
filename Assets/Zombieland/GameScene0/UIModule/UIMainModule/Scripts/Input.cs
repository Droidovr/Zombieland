using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class Input : MonoBehaviour
    {
        public event Action<Vector2> OnMoved;
        public event Action<string> OnButtonClick;

        private InputSystemControls _inputSystemControls;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
            _inputSystemControls.Main.Fire.performed += context => ButtonClick(NameButton.Fire);
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
            _inputSystemControls.Main.Fire.performed -= context => ButtonClick(NameButton.Fire);
            _inputSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnMoved?.Invoke(joistickPosition);
        }

        private void ButtonClick(NameButton nameButton)
        {
            OnButtonClick?.Invoke(nameButton.ToString());
        }
    }
}
