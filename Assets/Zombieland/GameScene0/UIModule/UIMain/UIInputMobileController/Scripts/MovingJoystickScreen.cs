using UnityEngine;
using UnityEngine.EventSystems;

namespace Zombieland.GameScene0.UIModule
{
    public class MovingJoystickScreen : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public delegate void TouchStarted(Vector2 touchPosition);
        public event TouchStarted OnTouchStarted;

        public delegate void TouchEnded();
        public event TouchEnded OnTouchEnded;

        private void OnPointerDown(PointerEventData eventData)
        {
            // Обработка начала касания
            Vector2 touchPosition = eventData.position;
            OnTouchStarted?.Invoke(touchPosition);
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            // Обработка завершения касания
            OnTouchEnded?.Invoke();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}
