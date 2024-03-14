using System;
using UnityEngine;
using UnityEngine.UI;

namespace Zombieland.GameScene0.UIModule
{
    public class Input : MonoBehaviour
    {
        public event Action<Vector2> OnMoved;
        public event Action OnFire;

        [SerializeField] private bool _isClickedStealthOn = false;
        [SerializeField] private Image _imageButtonStealth;

        private const string NAME_SPRITE_STEALTH_ON = "sit_down_on";
        private const string NAME_SPRITE_STEALTH_OFF = "sit_down_off";

        private InputSystemControls _inputSystemControls;
        private Sprite _spriteStealthOn;
        private Sprite _spriteStealthOff;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
        }

        private void Start()
        {
            _spriteStealthOn = Resources.Load<Sprite>(NAME_SPRITE_STEALTH_ON);
            _spriteStealthOff = Resources.Load<Sprite>(NAME_SPRITE_STEALTH_OFF);
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();
            _inputSystemControls.Main.Move.performed += context => Move();
            _inputSystemControls.Main.Move.canceled += context => Move();
            _inputSystemControls.Main.Fire.performed += context => FireClick();
            _inputSystemControls.Main.Stealth.performed += context => StealthClick();
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
            _inputSystemControls.Main.Fire.performed -= context => FireClick();
            _inputSystemControls.Main.Stealth.performed -= context => StealthClick();
            _inputSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnMoved?.Invoke(joistickPosition);
        }

        private void FireClick()
        {
            OnFire?.Invoke();
        }

        private void StealthClick()
        {
            _isClickedStealthOn = !_isClickedStealthOn;

            //_imageButtonStealth.sprite = _isClickedStealthOn ? _spriteStealthOn : _spriteStealthOff;
        }
    }
}
