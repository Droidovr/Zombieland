using UnityEngine;


namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    {
        public float MovingSpeed
        {
            set
            {
                if (value >= 0)
                { 
                    _movingSpeed = value;
                }
            }
        }
        public float MovingRotation 
        {
            set
            {
                if (value >= 0)
                { 
                    _movingRotation = value;
                }
            }
        }
        public CharacterMovingController CharacterMovingController
        { 
            get { return _characterMovingController; }
            set { _characterMovingController = value; }
        }


        private float _movingSpeed;
        private float _movingRotation;

        private Rigidbody _rigidbody;
        private CharacterMovingController _characterMovingController;

        private float _minDirectionJoustikMagnitude = 0.01f;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (CharacterMovingController.DirectionMove.magnitude < _minDirectionJoustikMagnitude)
                return;

            Vector3 movement = new Vector3(CharacterMovingController.DirectionMove.x, 0f, CharacterMovingController.DirectionMove.y);

            _rigidbody.velocity = movement * _movingSpeed;

            Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, toRotation, _movingRotation));

            _rigidbody.angularVelocity = Vector3.zero;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            float lengthLineDraw = 5f;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * lengthLineDraw);
        }
#endif
    }
}