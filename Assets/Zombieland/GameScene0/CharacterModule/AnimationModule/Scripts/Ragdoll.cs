using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class Ragdoll
    {
        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private List<Rigidbody> _rigidbodies;

        public Ragdoll(GameObject gameObject)
        {
            _animator = gameObject.GetComponent<Animator>();

            _unityCharacterController = gameObject.GetComponent<UnityEngine.CharacterController>();

            _rigidbodies = new List<Rigidbody>(gameObject.GetComponentsInChildren<Rigidbody>());

            Disable();
        }

        public async void Hit(Vector3 force, Vector3 hitPosition)
        {
            Enable();

            Rigidbody injuredRigidbody = _rigidbodies.OrderBy(rigidbody => Vector3.Distance(rigidbody.position, hitPosition)).First();
            injuredRigidbody.AddForceAtPosition(force, hitPosition, ForceMode.Impulse);

            //await Task.Delay(300);

            //Disable();
        }

        public void Enable()
        {
            _animator.enabled = false;
            _unityCharacterController.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        public void Disable()
        {
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }

            _unityCharacterController.enabled = true;
            _animator.enabled = true;
        }
    }
}