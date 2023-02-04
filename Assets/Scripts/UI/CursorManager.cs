using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Cursor = UnityEngine.Cursor;

namespace RootieSmoothie.UI
{
    public class CursorManager : Singleton<CursorManager>
    {
        public enum CursorState
        {
            Default,
            Click
        }

        private CursorState _state;
        
        [SerializeField]
        private Texture2D _cursorDefaultSprite;
        [SerializeField]
        private Texture2D _cursorDefaultClickSprite;

        private Texture2D _currentCursorSprite;
        private Texture2D _currentCursorClickSprite;
        
        [Header("Pivot")]
        [SerializeField]
        private Vector2 _defaultPivot;
        [SerializeField]
        private Vector2 _pivot;
        
        private bool _isMouseButtonDown;

        public void Start()
        {
            ResetCursorToDefault();
        }

        public void ResetCursorToDefault()
        {
            SetCursorSprites(_cursorDefaultSprite, _cursorDefaultClickSprite, _defaultPivot);
            SetCursor(GetCursorTexture());
        }

        public void SetCursorSprites(Texture2D normal, Texture2D click, Vector2 pivot)
        {
            _pivot = pivot;
            _currentCursorSprite = normal;
            _currentCursorClickSprite = click;
            SetCursor(GetCursorTexture());
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
            SetCursor(_currentCursorSprite);
            _state = CursorState.Default;
        }

        private void SetClickCursor()
        {
            SetCursor(_currentCursorClickSprite);
            _state = CursorState.Click;
        }

        private Texture2D GetCursorTexture()
        {
            switch (_state)
            {
                case CursorState.Click:
                    return _currentCursorClickSprite;
                default:
                    return _currentCursorSprite;
            }
        }

        private void SetCursor(Texture2D texture)
        {
            var pixelPos = _pivot * new Vector2(texture.width, texture.height);
            pixelPos.y = texture.height - pixelPos.y;
            Cursor.SetCursor(texture, pixelPos, CursorMode.ForceSoftware);
        }
    }
}
