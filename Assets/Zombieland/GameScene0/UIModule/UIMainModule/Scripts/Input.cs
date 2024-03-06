using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zombieland.GameScene0.UIModule
{
    public class Input : MonoBehaviour
    {
        public event Action<Vector2> OnMoved;
        public event Action OnFireDown;
        public event Action OnFireUp;
        public bool IsClickedStealthOn = false;

        private const string NAME_SPRITE_STEALTH_ON = "StealthOn";
        private const string NAME_SPRITE_STEALTH_OFF = "StealthOff";

        private InputSystemControls _inputSystemControls;
        private Image _imageButtonStealth;
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
            _inputSystemControls.Main.Fire.performed += context => FireClickDown();
            _inputSystemControls.Main.Fire.canceled += context => FireClickUp();
            _inputSystemControls.Main.Stealth.performed += context => StealthClick();
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
            _inputSystemControls.Main.Fire.performed -= context => FireClickDown();
            _inputSystemControls.Main.Fire.canceled -= context => FireClickUp();
            _inputSystemControls.Main.Stealth.performed -= context => StealthClick();
            _inputSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnMoved?.Invoke(joistickPosition);
        }

        private void FireClickDown()
        {
            OnFireDown?.Invoke();
        }

        private void FireClickUp()
        {
            OnFireUp?.Invoke();
        }

        private void StealthClick()
        {
            IsClickedStealthOn = !IsClickedStealthOn;

            _imageButtonStealth.sprite = IsClickedStealthOn ? _spriteStealthOn : _spriteStealthOff;
        }
    }
}
