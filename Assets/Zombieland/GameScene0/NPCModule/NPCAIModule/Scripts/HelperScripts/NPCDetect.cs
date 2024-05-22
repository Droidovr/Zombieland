using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCDetect : MonoBehaviour
    {
        private const float INVOKE_REPEATING_TIME = 0.1f;

        private INPCAIController _nPCAIController;
        private NavMeshAgent _navMeshAgent;
        private Transform _transformDestenation;

        public void Init(INPCAIController nPCAIController)
        {
            _nPCAIController = nPCAIController;
            _navMeshAgent = _nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<NavMeshAgent>();
        }

        public void StartDestenation(Transform transformDestenation)
        {
            _transformDestenation = transformDestenation;
            InvokeRepeating(nameof(UpdateDestenation), 0f, INVOKE_REPEATING_TIME);
        }

        public void StopDestenation()
        {
            CancelInvoke(nameof(UpdateDestenation));
        }

        private void UpdateDestenation()
        {
            _navMeshAgent.SetDestination(_transformDestenation.position);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(UpdateDestenation));
        }
    }
}