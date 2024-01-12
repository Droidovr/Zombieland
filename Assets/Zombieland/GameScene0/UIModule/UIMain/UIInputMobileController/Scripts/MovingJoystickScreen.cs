using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Zombieland.GameScene0.UIModule
{
    public class MovingJoystickScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private OnScreenStick _onScreenStick;

        private Vector2 _startPosition;

        private void Start()
        {
            _startPosition = _rectTransform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;
            _onScreenStick.OnPointerDown(eventData);
            Debug.Log(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.position = _startPosition;
            Debug.Log(eventData.position);
        }
    }
}