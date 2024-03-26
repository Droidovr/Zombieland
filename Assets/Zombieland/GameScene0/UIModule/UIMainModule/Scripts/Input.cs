using System;
using UnityEngine;

namespace Zombieland.GameScene0.UIModule
{
    public class Input : MonoBehaviour
    {
        public event Action<Vector2> OnMoved;
        public event Action<bool> OnFire;
        public event Action<bool> OnStealth;
        public event Action OnWeaponReaload;
        public event Action OnUseE;
        public event Action<bool> OnFastRun;
        public event Action OnThrow;
        public event Action OnFists;
        public event Action OnKnife;
        public event Action OnPistol;
        public event Action OnShotgun;
        public event Action OnHealing;

        private InputSystemControls _inputSystemControls;

        private void Awake()
        {
            _inputSystemControls = new InputSystemControls();
        }

        private void OnEnable()
        {
            _inputSystemControls.Enable();
            _inputSystemControls.Main.Move.performed += context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            _inputSystemControls.Main.Move.canceled += context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            
            _inputSystemControls.Main.Fire.performed += context => OnFire?.Invoke(true);
            _inputSystemControls.Main.Fire.canceled += context => OnFire?.Invoke(false);
            
            _inputSystemControls.Main.Stealth.performed += context => OnStealth?.Invoke(true);
            _inputSystemControls.Main.Stealth.canceled += context => OnStealth?.Invoke(false);
            
            _inputSystemControls.Main.WeaponRealod.performed += context => OnWeaponReaload?.Invoke();
            
            _inputSystemControls.Main.Use.performed += context => OnUseE?.Invoke();
            
            _inputSystemControls.Main.FastRun.performed += context => OnFastRun?.Invoke(true);
            _inputSystemControls.Main.FastRun.canceled += context => OnFastRun?.Invoke(false);
            
            _inputSystemControls.Main.Throw.performed += context => OnThrow?.Invoke();
            
            _inputSystemControls.Main.Fists.performed += context => OnFists?.Invoke();
            
            _inputSystemControls.Main.Knife.performed += context => OnKnife?.Invoke();
            
            _inputSystemControls.Main.Pistol.performed += context => OnPistol?.Invoke();
            
            _inputSystemControls.Main.Shotgun.performed += context => OnShotgun?.Invoke();
            
            _inputSystemControls.Main.Healing.performed += context => OnHealing?.Invoke();
        }
        private void OnDisable()
        {
            _inputSystemControls.Main.Move.performed -= context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            _inputSystemControls.Main.Move.canceled -= context => OnMoved?.Invoke(_inputSystemControls.Main.Move.ReadValue<Vector2>());
            
            _inputSystemControls.Main.Fire.performed -= context => OnFire?.Invoke(true);
            _inputSystemControls.Main.Fire.canceled -= context => OnFire?.Invoke(false);
            
            _inputSystemControls.Main.Stealth.performed -= context => OnStealth?.Invoke(true);
            _inputSystemControls.Main.Stealth.canceled -= context => OnStealth?.Invoke(false);
            
            _inputSystemControls.Main.WeaponRealod.performed -= context => OnWeaponReaload?.Invoke();
            
            _inputSystemControls.Main.Use.performed -= context => OnUseE?.Invoke();
            
            _inputSystemControls.Main.FastRun.performed -= context => OnFastRun?.Invoke(true);
            _inputSystemControls.Main.FastRun.canceled -= context => OnFastRun?.Invoke(false);
            
            _inputSystemControls.Main.Throw.performed -= context => OnThrow?.Invoke();
            
            _inputSystemControls.Main.Fists.performed -= context => OnFists?.Invoke();
            
            _inputSystemControls.Main.Knife.performed -= context => OnKnife?.Invoke();
            
            _inputSystemControls.Main.Pistol.performed -= context => OnPistol?.Invoke();
            
            _inputSystemControls.Main.Shotgun.performed -= context => OnShotgun?.Invoke();
            
            _inputSystemControls.Main.Healing.performed -= context => OnHealing?.Invoke();
            
            _inputSystemControls.Disable();
        }
    }
}
