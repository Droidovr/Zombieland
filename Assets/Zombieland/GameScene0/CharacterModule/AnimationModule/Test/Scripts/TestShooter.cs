using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestShooter : MonoBehaviour
    {
        public event Action OnShoot;

        private float _force = 50f;
        private Camera _camera;

        private CharacterRagdoll _characterRagdoll;


        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (_characterRagdoll != null)
                    {
                        Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
                        forceDirection.y = 0;
                        _characterRagdoll.Hit(forceDirection * _force, hit.point, true);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _characterRagdoll.GetUp();
            }
        }

        public void Init(CharacterRagdoll characterRagdoll)
        {
            _characterRagdoll = characterRagdoll;
        }
    }
}