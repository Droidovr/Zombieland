using System;
using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCFire : MonoBehaviour
    {
        public event Action<bool> OnFire;

        private const float INVOKE_REPEATING_TIME = 0.5f;

        private Transform _characterTransform;
        private Transform _nPCTransform;
        private NavMeshAgent _navMeshAgent;
        private bool _isFire;


        public void Init(INPCAIController nPCAIController)
        {
            _characterTransform = nPCAIController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            _nPCTransform = nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();

            InvokeRepeating(nameof(CheckFire), 0f, INVOKE_REPEATING_TIME);
        }

        private void CheckFire()
        {
            if (Vector3.Distance(_characterTransform.position, _nPCTransform.position) <= _navMeshAgent.stoppingDistance + 0.3f)
            {
                OnFire?.Invoke(true);
                _isFire = true;
                Debug.Log("Attack");
            }
            else
            {
                if (_isFire)
                {
                    OnFire?.Invoke(false);
                    _isFire = false;
                    Debug.Log("Finish Attack");
                }
            }
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(CheckFire));
        }
    }
}