using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class TestInput : MonoBehaviour
    {
        public Vector2 InputVectorMove 
        { 
            get { return _inputVectorMove; } 
        }

        private Vector2 _inputVectorMove;

        private void Update()
        {
            _inputVectorMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}