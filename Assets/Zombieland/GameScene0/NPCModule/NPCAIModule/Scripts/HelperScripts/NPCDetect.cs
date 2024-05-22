using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCDetect : MonoBehaviour
    {
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
            InvokeRepeating(nameof(UpdateDestenation), 0f, 0.1f);
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