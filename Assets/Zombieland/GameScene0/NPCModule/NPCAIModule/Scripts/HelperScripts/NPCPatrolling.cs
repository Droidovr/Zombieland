using UnityEngine.AI;
using UnityEngine;


namespace Zombieland.GameScene0.NPCModule.NPCAIModule
{
    public class NPCPatrolling : MonoBehaviour
    {
        private INPCAIController _nPCAIController;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _positionSpawn;
        private Vector3 _positionPatrol;
        private bool isGoingToPositionSpawn = false;


        public void Init(INPCAIController nPCAIController) 
        {
            _nPCAIController = nPCAIController;
            _navMeshAgent = _nPCAIController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<NavMeshAgent>();

            System.Numerics.Vector3 positionSpawn = _nPCAIController.NPCController.NPCDataController.NPCData.NPCSpawnData.SpawnPosition;
            _positionSpawn = new Vector3(positionSpawn.Z, positionSpawn.Y, positionSpawn.Z);

            System.Numerics.Vector3 positionPatrol = _nPCAIController.NPCController.NPCDataController.NPCData.NPCSpawnData.PatrolPoint;
            _positionPatrol = new Vector3(positionPatrol.X, positionPatrol.Y, positionPatrol.Z);

            InvokeRepeating("CheckDestination", 0f, 0.5f);
        }
        private void CheckDestination() 
        {
            if (!_nPCAIController.IsPatrolling)
                return;

            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.51f)
            {
                if (isGoingToPositionSpawn)
                {
                    isGoingToPositionSpawn = false;
                    _navMeshAgent.SetDestination(_positionPatrol);
                }
                else
                {
                    isGoingToPositionSpawn = true;
                    _navMeshAgent.SetDestination(_positionSpawn);
                }
            }
        }
    }
}