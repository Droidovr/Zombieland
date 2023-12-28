using UnityEngine;


namespace Zombieland.CharacterModule.CharacterMovingModule
{
    public class CharacterPhysicMoving : MonoBehaviour
    { 
        //private Rigidbody _rigidbody;
        //private CharacterMoovingController _characterMovingController;
        //private Vector2 _directionJoustik;
        //private PhysicCharacterProperties _physicCharacterProperties;

        //private float _minDirectionJoustikMagnitude = 0.01f;

        //public bool Initialize(CharacterMoovingController parentController)
        //{
        //    _characterMovingController = parentController;
        //    _directionJoustik = parentController.DirectionMove;
        //    _physicCharacterProperties = parentController.PhysicCharacterProperties;

        //    if (_characterMovingController != null && _directionJoustik != null)
        //    { 
                
        //    }

        //    return true;
        //}

        //private void FixedUpdate()
        //{
        //    if (_directionJoustik.magnitude < _minDirectionJoustikMagnitude)
        //        return;

        //    Vector3 movement = new Vector3(_directionJoustik.x, 0f, _directionJoustik.y);

        //    _rigidbody.velocity = movement * _physicCharacterProperties.MovingSpeed;

        //    Quaternion toRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);
        //    _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, toRotation, _physicCharacterProperties.MovingRotation));
        //}

        //#if UNITY_EDITOR
        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    float lengthLineDraw = 5f;
        //    Gizmos.DrawLine(transform.position, transform.position + transform.forward * lengthLineDraw);
        //}
        //#endif
    }
}