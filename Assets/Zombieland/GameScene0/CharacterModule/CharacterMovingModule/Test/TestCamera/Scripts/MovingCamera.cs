using UnityEngine;


namespace Zombieland.GameScene0.CharacterModule.CharacterMovingModule
{
    public class MovingCamera : MonoBehaviour
    {
        public GameObject Character;

        private float _smoothSpeedCamera = 0.1f;
        private Vector3 _startPositionCamera;

        private void Awake()
        {
            _startPositionCamera = transform.position;
        }

        private void FixedUpdate()
        {
            if (Character == null)
                return;

            Vector3 desiredPosition = Character.transform.position + _startPositionCamera;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeedCamera);
            transform.position = smoothedPosition;
        }
    }
}