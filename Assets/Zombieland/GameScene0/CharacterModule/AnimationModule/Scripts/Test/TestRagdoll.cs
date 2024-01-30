using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestRagdoll : MonoBehaviour
    {
        [SerializeField] private bool _isRagdoll;

        private Animator _animator;
        private UnityEngine.CharacterController _unityCharacterController;
        private List<Rigidbody> _rigidbodies;


        private void Update()
        {
            if (_isRagdoll)
            {
                EnableRagdoll();
            }
            else
            { 
                DisableRagdoll();
            }
        }

        public void Init()
        {
            _animator = GetComponent<Animator>();
            _unityCharacterController = GetComponent<UnityEngine.CharacterController>();
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        }

        private void EnableRagdoll()
        {
            _animator.enabled = false;
            _unityCharacterController.enabled = false;

            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
        }

        private void DisableRagdoll()
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