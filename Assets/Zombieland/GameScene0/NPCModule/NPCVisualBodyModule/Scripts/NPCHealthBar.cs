using UnityEngine;
using UnityEngine.UI;


namespace Zombieland.GameScene0.NPCModule.NPCVisualBodyModule
{
    public class NPCHealthBar : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] Slider _slider;

        private INPCVisualBodyController _nPCVisualBodyController;


        public void Init(INPCVisualBodyController nPCVisualBodyController)
        {
            _nPCVisualBodyController = nPCVisualBodyController;
            _canvas.worldCamera = _nPCVisualBodyController.NPCController.NPCManagerController.RootController.CameraController.PlayerCamera;
            _slider.value = CalculateHealthProcent();

            _nPCVisualBodyController.NPCController.NPCTakeDamageController.OnApplyImpact += UpdateHealth;
        }

        private void UpdateHealth(Vector3 impactCollisionPosition, Vector3 impactDirection)
        {
            Debug.Log("UpdateHealth: " + _nPCVisualBodyController.NPCController.NPCDataController.NPCData.CurrentHealth);

            _slider.value = CalculateHealthProcent();

            Debug.Log("_slider.value: " + _slider.value + " / " + CalculateHealthProcent());

            if (_nPCVisualBodyController.NPCController.NPCDataController.NPCData.CurrentHealth <= 0)
            {
                _canvas.gameObject.SetActive(false);
            }
        }

        private float CalculateHealthProcent()
        {
            return _nPCVisualBodyController.NPCController.NPCDataController.NPCData.CurrentHealth / _nPCVisualBodyController.NPCController.NPCDataController.NPCData.MaxHealth;
        }
    }
}