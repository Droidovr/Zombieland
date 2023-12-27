using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestInput : MonoBehaviour
    {
        private ICharacterMovingController _characterMovingController;

        private void Start()
        {
            _characterMovingController = GetComponent<ICharacterMovingController>();
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            
            if (moveInput.magnitude > 0.1f)
                _characterMovingController.Move(moveInput);
        }
    }
}