using UnityEngine;
using UnityEngine.EventSystems;
using Cursor = UnityEngine.Cursor;

namespace RootieSmoothie.UI
{
    public class CursorManager : Singleton<CursorManager>
    {
        [SerializeField]
        private Texture2D _cursorDefaultSprite;
        [SerializeField]
        private Texture2D _cursorClickSprite;

        private bool _isMouseButtonDown;

        public void Awake()
        {
            SetDefaultCursor();
        }

        public void Update()
        {
            var changed = false;
            if (_isMouseButtonDown != Input.GetMouseButtonDown(0))
            {
                _isMouseButtonDown = true;
                changed = true;
            }
            if (_isMouseButtonDown && Input.GetMouseButtonUp(0))
            {
                _isMouseButtonDown = false;
                changed = true;
            }

            if (!changed) return;
            if (_isMouseButtonDown)
            {
                SetClickCursor();
            }
            else
            {
                SetDefaultCursor();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SetClickCursor();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetDefaultCursor();
        }

        private void SetDefaultCursor()
        {
            Cursor.SetCursor(_cursorDefaultSprite, new Vector2(0.33f, 0.25f), CursorMode.ForceSoftware);
        }

        private void SetClickCursor()
        {
            Cursor.SetCursor(_cursorClickSprite, new Vector2(0.33f, 0.25f), CursorMode.ForceSoftware);
        }
    }
}
