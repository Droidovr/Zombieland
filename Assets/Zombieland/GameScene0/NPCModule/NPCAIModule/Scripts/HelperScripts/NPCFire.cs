using System;
using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCFire : MonoBehaviour
    {
        public event Action<bool> OnFire;

        private const float FIELD_OF_VIEW = 60f;

        private Transform _characterTransform;
        private Transform _nPCTransform;
        private NavMeshAgent _navMeshAgent;
        private bool _isFire;


        public void Init(INPCAIController nPCAIController)
        {
            _characterTransform = nPCAIController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            _nPCTransform = nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }


        private void Update()
        {
            Vector3 directionToCharacter = (_characterTransform.position - _nPCTransform.position).normalized;
            float distanceToCharacter = Vector3.Distance(_characterTransform.position, _nPCTransform.position);

            if (distanceToCharacter <= _navMeshAgent.stoppingDistance + 0.3f)
            {
                float dotProduct = Vector3.Dot(_nPCTransform.forward, directionToCharacter);
                float angleToCharacter = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;

                if (angleToCharacter <= FIELD_OF_VIEW / 2)
                {
                    OnFire?.Invoke(true);
                    _isFire = true;
                }
                else
                {
                    if (_isFire)
                    {
                        OnFire?.Invoke(false);
                        _isFire = false;
                    }
                }
            }
            else
            {
                if (_isFire)
                {
                    OnFire?.Invoke(false);
                    _isFire = false;
                }
            }
        }
    }
}