using System;
using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AnimationModule
{
    public class TestShooter : MonoBehaviour
    {
        public event Action OnShoot;

        private float _force = 100;
        private Camera _camera;

        private Ragdoll _ragdoll;


        private void Start()
        {
            _camera = Camera.main;
        }

        public void Init(Ragdoll ragdoll)
        {
            _ragdoll = ragdoll;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (_ragdoll != null)
                    {
                        Vector3 forceDirection = (hit.point - _camera.transform.position).normalized;
                        forceDirection.y = 0;

                        _ragdoll.Hit(forceDirection * _force, hit.point);
                    }
                }
            }
        }
    }
}