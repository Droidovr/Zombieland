using UnityEngine;

namespace Zombieland.GameScene0.CharacterModule.AimingModule
{
    public class Aiming
    {
        private IAimingController _aimingController;

        public Aiming(IAimingController aimingController) 
        {
            _aimingController = aimingController;
        }

        public void Disable()
        {
            //_aimingController.CharacterController.RootController.UIController.OnMouseMoved += MouseMovedHandler;
            //_aimingController.CharacterController.RootController.CameraController.PlayerCamera;
            //_aimingController.CharacterController.VisualBodyController.CharacterInScene;

            // ֳ�� �� ������ �� ��� ��������� Impactable, ����� ������� �� ��� ������� GetComponent<Impactable>() -
            // ���� ��� - �� ���������� ���, ���� � - �� ��������� ��������� ������ ��"���� ����� �� �������.
        }

        public Transform GetTarget()
        { 
            return default(Transform);
        }
    }
}