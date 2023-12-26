using UnityEngine;

namespace Zombieland.CharacterModule.CharacterMovingModule
{
    [RequireComponent(typeof(Rigidbody))]
    public class CharacterMoovingController : MonoBehaviour, ICharacterMovingController
    {
        // змінні будуть заповнюватись з 
        private float _moveSpeed = 500f;
        private float _rotationSpeed = 5f;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void Start()
        {
            // маємо підгрузити з зовні параметри _moveSpeed та _rotationSpeed
        }

        public void Move(Vector2 direction)
        {
            Vector3 movement = new Vector3(direction.x, 0f, direction.y);

            _rigidbody.velocity = movement * _moveSpeed * Time.fixedDeltaTime;

            Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, toRotation, _rotationSpeed * Time.fixedDeltaTime));
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            float lengthLineDraw = 5f;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * lengthLineDraw);
        }
        #endif
    }
}
