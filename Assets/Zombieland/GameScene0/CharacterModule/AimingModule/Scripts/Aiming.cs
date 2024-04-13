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

            // Ціль має містити на собі компонент Impactable, тобто потрібно на цілі зробити GetComponent<Impactable>() -
            // якщо нул - то повертаэмо нул, якщо ні - то повертаємо трансформ нашого об"єкту якого ми знайшли.
        }

        public Transform GetTarget()
        { 
            return default(Transform);
        }
    }
}