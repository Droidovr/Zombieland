using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestRagdoll : MonoBehaviour
    {
        [SerializeField] private bool _isRagdoll;

        private Animator _animator;
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
            _rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        }

        private void EnableRagdoll()
        {
            _animator.enabled = false;

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

            _animator.enabled = true;
        }
    }
}