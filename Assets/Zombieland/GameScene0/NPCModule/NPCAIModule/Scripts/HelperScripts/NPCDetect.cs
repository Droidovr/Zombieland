using UnityEngine;
using UnityEngine.AI;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCDetect : MonoBehaviour
    {
        private INPCAIController _nPCAIController;
        private NavMeshAgent _navMeshAgent;


        public void Init(INPCAIController nPCAIController)
        {
            _nPCAIController = nPCAIController;
            _navMeshAgent = _nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<NavMeshAgent>();
        }

        public void SetDestenation(Vector3 positionSestenation)
        {
            _navMeshAgent.SetDestination(positionSestenation);
        }
    }
}