using System;
using UnityEngine;
using Zombieland.GameScene0.CharacterModule;

namespace Zombieland.GameScene0.NPCModule.NPCAwarenessModule.NPCHearingModule
{
    public class HearingSensor
    {
        public event Action<IController, bool> OnHearingDetect;

        private INPCHearingController _nPCHearingController;
        private LayerMask _wallLayer = LayerMask.GetMask("Wall");
        public LayerMask groundLayer = LayerMask.GetMask("Ground");
        private int _vertexCount = 40;
        private float _lineWidth = 0.1f;
        private LineRenderer _lineRenderer;
        private bool isActive = true;
        private bool isDetect;

        public HearingSensor(INPCHearingController nPCHearingController)
        {
            _nPCHearingController = nPCHearingController;
            _lineRenderer = _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.GetComponent<LineRenderer>();

            _nPCHearingController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.SoundBurstController.OnSound += CharacterSoundReactionHandler;
            _nPCHearingController.NPCAwarenessController.NPCController.NPCManagerController.RootController.CharacterController.StealthController.OnStealth += CharacterStealthHandler;
            _nPCHearingController.NPCAwarenessController.NPCController.NPCMovingController.OnMoving += DrawCirclehandler;

        }

        private void CharacterStealthHandler(bool isCharacterStealth)
        {
            _lineRenderer.enabled = isCharacterStealth;
            isActive = !isCharacterStealth;
        }

        private void CharacterSoundReactionHandler(IController controller)
        {
            if (!isActive)
            { 
                return;
            }
            
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
                        if (!isDetect)
                        {
                            isDetect = true;
                            OnHearingDetect?.Invoke(controller, isDetect);
                            Debug.Log("OnHearingDetect: " + characterController.VisualBodyController.CharacterInScene.name);
                        }
                    }
                }
                else
                {
                    if (isDetect)
                    {
                        isDetect = false;
                        OnHearingDetect?.Invoke(controller, isDetect);
                        Debug.Log("Out of earshot - OnHearingDetect: " + characterController.VisualBodyController.CharacterInScene.name);
                    }
                }
            }
        }

        private void DrawCirclehandler(float arg1, bool arg2)
        {
            if (isActive)
            {
                return;
            }

            _lineRenderer.positionCount = _vertexCount + 1; // ”величиваем на 1, чтобы добавить точку круга в конце
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;

            Vector3 nPCPosition = _nPCHearingController.NPCAwarenessController.NPCController.NPCVisualBodyController.NPCInScene.transform.position;

            for (int i = 0; i < _vertexCount; i++)
            {
                float angle = i * Mathf.PI * 2 / _vertexCount;
                float x = nPCPosition.x + Mathf.Cos(angle) * _nPCHearingController.NPCAwarenessController.NPCController.NPCDataController.NPCData.HearingDistance;
                float z = nPCPosition.z + Mathf.Sin(angle) * _nPCHearingController.NPCAwarenessController.NPCController.NPCDataController.NPCData.HearingDistance;
                Vector3 circlePoint = new Vector3(x, 0.03f, z);
                _lineRenderer.SetPosition(i, circlePoint);
            }

            _lineRenderer.SetPosition(_vertexCount, _lineRenderer.GetPosition(0));
        }

    }
}