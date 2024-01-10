using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class InputManager : MonoBehaviour
    {
        public event Action<Vector2> OnJoystickMoved;
        public event Action OnFire;

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
            _inputSystemControls.Main.Fire.performed += context => Fire();
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => Move();
            _inputSystemControls.Main.Move.canceled -= context => Move();
            _inputSystemControls.Main.Fire.performed -= context => Fire();
            _inputSystemControls.Disable();
        }
        
        private void Move()
        {
            Vector2 joistickPosition = _inputSystemControls.Main.Move.ReadValue<Vector2>();
            
            OnJoystickMoved?.Invoke(joistickPosition);
        }

        private void Fire()
        { 
            OnFire?.Invoke();
            Debug.Log("Pif-Paf ... Pif-Paf ... Pif-Paf !!!!");
        }
    }
}
