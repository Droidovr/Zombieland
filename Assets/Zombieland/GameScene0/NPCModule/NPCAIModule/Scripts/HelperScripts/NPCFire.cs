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
        private bool _wasInRange;
        private bool _wasInFieldOfView;


        public void Init(INPCAIController nPCAIController)
        {
            _characterTransform = nPCAIController.NPCController.NPCManagerController.RootController.CharacterController.VisualBodyController.CharacterInScene.transform;
            _nPCTransform = nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.transform;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            bool isInRange = IsCharacterInRange();
            bool isInFieldOfView = IsInFieldOfView();

            if (isInRange && !_wasInRange)
            {
                // Вошли в зону атаки
                OnFire?.Invoke(true);
            }
            else if (!isInRange && _wasInRange)
            {
                // Вышли из зоны атаки
                OnFire?.Invoke(false);
            }
            else if (isInRange && _wasInRange)
            {
                // Внутри зоны атаки
                if (isInFieldOfView && !_wasInFieldOfView)
                {
                    // Вошли в поле зрения
                    OnFire?.Invoke(true);
                }
                else if (!isInFieldOfView && _wasInFieldOfView)
                {
                    // Вышли из поля зрения
                    OnFire?.Invoke(false);
                }
            }

            // Обновляем состояние
            _wasInRange = isInRange;
            _wasInFieldOfView = isInFieldOfView;
        }


        private bool IsCharacterInRange()
        {
            Vector3 directionToCharacter = (_characterTransform.position - _nPCTransform.position).normalized;
            float distanceToCharacter = Vector3.Distance(_characterTransform.position, _nPCTransform.position);
            return distanceToCharacter <= _navMeshAgent.stoppingDistance + 0.1f;
        }

        private bool IsInFieldOfView()
        {
            Vector3 directionToCharacter = (_characterTransform.position - _nPCTransform.position).normalized;
            float dotProduct = Vector3.Dot(_nPCTransform.forward, directionToCharacter);
            float angleToTarget = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
            return angleToTarget <= FIELD_OF_VIEW / 2;
        }

    }
}