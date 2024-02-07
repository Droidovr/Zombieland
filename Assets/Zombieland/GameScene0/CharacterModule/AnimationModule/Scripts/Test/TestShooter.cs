using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestShooter : MonoBehaviour
    {
        public event Action OnShoot;

        private float _force = 20f;
        private Camera _camera;

        private CharacterRagdoll _characterRagdoll;


        private void Start()
        {
            _camera = Camera.main;
        }

        public void Init(CharacterRagdoll characterRagdoll)
        {
            _characterRagdoll = characterRagdoll;
        }

        // Update is called once per frame
        void Update()
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
                        _characterRagdoll.Hit(forceDirection * _force, hit.point);
                        //_characterRagdoll.ActivateRagdoll();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _characterRagdoll.DeactivateRagdoll();
            }
        }
    }
}