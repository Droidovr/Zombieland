using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public class HearingSensor
    {
        private INPCHearingController _nPCHearingController;
        private LayerMask _wallLayer = LayerMask.GetMask("Wall");
        public LayerMask groundLayer = LayerMask.GetMask("Ground");
        private int _vertexCount = 40; // количество вершин
        private float _lineWidth = 0.1f; // ширина линии
        private LineRenderer _lineRenderer;

        public HearingSensor(INPCHearingController nPCHearingController)
        {
            _nPCHearingController = nPCHearingController;

            _nPCHearingController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.SoundBurstController.OnSound += CharacterSoundReaction;

            _lineRenderer = _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<LineRenderer>();

            DrawCircle();
        }

        private void CharacterSoundReaction(IController controller)
        {
            if (controller is ICharacterController characterController)
            {
                float distance = Vector3.Distance
                    (
                        _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.position,
                        characterController.VisualBodyController.CharacterInScene.transform.position
                    );

                if (distance <= _nPCHearingController.NPCAwarenessController.NPCController.NPCDataController.NPCData.HearingDistance)
                {
                    Vector3 direction = _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.position -
                        characterController.VisualBodyController.CharacterInScene.transform.position;

                    Ray ray = new Ray(_nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.position, direction);

                    RaycastHit hit;
                    bool isHit = Physics.Raycast(ray, out hit, direction.magnitude);

                    if (isHit && ((1 << hit.collider.gameObject.layer) & _wallLayer) == 0)
                    {
                        Debug.Log("Луч долетел до препятствия, но на его пути не было стены.");
                    }
                }
            }
        }

        private void DrawCircle()
        {
            RaycastHit hit;
            Vector3 castOrigin = _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.position;

            _lineRenderer.positionCount = _vertexCount;
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;

            for (int i = 0; i < _vertexCount; i++)
            {
                float angle = i * Mathf.PI * 2 / _vertexCount;
                float x = Mathf.Cos(angle) * _nPCHearingController.NPCAwarenessController.NPCController.NPCDataController.NPCData.HearingDistance;
                float z = Mathf.Sin(angle) * _nPCHearingController.NPCAwarenessController.NPCController.NPCDataController.NPCData.HearingDistance;
                Vector3 circlePoint = new Vector3(x, 0, z);

                if (Physics.Raycast(castOrigin + circlePoint + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
                {
                    circlePoint.y = hit.point.y + 0.3f; // Установка высоты круга в точке столкновения с поверхностью
                }

                _lineRenderer.SetPosition(i, circlePoint);
            }
        }
    }
}