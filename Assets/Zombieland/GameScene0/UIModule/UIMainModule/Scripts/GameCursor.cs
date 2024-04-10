using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

namespace Zombieland.GameScene0.UIModule
{
    public class GameCursor
    {
        public Vector2 SizeCursor { get; private set; }

        private const string CURSOR_DEFAULT_NAME = "UISprites/cursorDefault";
        private const string CURSOR_AIM_NAME = "UISprites/cursorAim";

        private Texture2D _cursorDefaultTexture;
        private Texture2D _cursorAimTexture;
        private IUIMainController _uIMainController;
        private List<RaycastResult> _raycastResults;
        private bool _isPointerOverGameObject = false;

        public GameCursor(IUIMainController uIMainController)
        {
            _cursorDefaultTexture = Resources.Load<Texture2D>(CURSOR_DEFAULT_NAME);
            _cursorAimTexture = Resources.Load<Texture2D>(CURSOR_AIM_NAME);

            SizeCursor = new Vector2(_cursorAimTexture.width, _cursorAimTexture.height);

            _uIMainController = uIMainController;
            _uIMainController.OnMouseMoved += UpdateCursor;

            _raycastResults = new List<RaycastResult>();

            Cursor.SetCursor(_cursorAimTexture, Vector2.zero, CursorMode.Auto);
        }

        public void Disable()
        {
            _uIMainController.OnMouseMoved -= UpdateCursor;
        }

        private void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOverGameObject = true;
        }

        private void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOverGameObject = false;
        }

        private void UpdateCursor(Vector2 mousePosition)
        {
            if (_isPointerOverGameObject)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = mousePosition;

                _raycastResults.Clear();

                EventSystem.current.RaycastAll(pointerData, _raycastResults);

                if (_raycastResults.Count > 0)
                {
                    if (_raycastResults[0].gameObject.GetComponent<OnScreenButton>() != null)
                    {
                        Cursor.SetCursor(_cursorDefaultTexture, Vector2.zero, CursorMode.Auto);
                    }
                }
                else
                {
                    Cursor.SetCursor(_cursorAimTexture, Vector2.zero, CursorMode.Auto);
                }
            }
        }
    }
}