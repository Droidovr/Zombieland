using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Zombieland.GameScene0.UIModule
{
    public class Input : MonoBehaviour
    {
        public event Action<Vector2> OnMoved;
        public event Action<string> OnButtonClick;

        private const string NAME_SPRITE_STEALTH_ON = "StealthOn";
        private const string NAME_SPRITE_STEALTH_OFF = "StealthOff";

        private InputSystemControls _inputSystemControls;
        private Image _imageButtonStealth;
        private bool isClickedStealthOn = false;
        private Sprite _spriteStealthOn;
        private Sprite _spriteStealthOff;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
        }

        private void Start()
        {
            _imageButtonStealth = transform.Find("Stealth").GetComponent<Image>();
            _spriteStealthOn = Resources.Load<Sprite>(NAME_SPRITE_STEALTH_ON);
            _spriteStealthOff = Resources.Load<Sprite>(NAME_SPRITE_STEALTH_OFF);
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
            _inputSystemControls.Main.Fire.performed += context => ButtonClick(NameButton.Fire);
            _inputSystemControls.Main.Stealth.performed += context => ButtonClick(NameButton.Stealth);
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
            _inputSystemControls.Main.Fire.performed -= context => ButtonClick(NameButton.Fire);
            _inputSystemControls.Main.Stealth.performed -= context => ButtonClick(NameButton.Stealth);
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

            if (nameButton == NameButton.Stealth)
            {
                isClickedStealthOn = !isClickedStealthOn;

                _imageButtonStealth.sprite = isClickedStealthOn ? _spriteStealthOn : _spriteStealthOff;
            }
        }
    }
}
