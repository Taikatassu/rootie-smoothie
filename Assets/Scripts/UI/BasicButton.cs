using RootieSmoothie.Audio;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Button), typeof(Image), typeof(AudioSource))]
public class BasicButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public Image _image;
    [SerializeField]
    private Button _button;

    [HideInInspector]
    public Object EventData;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _onHoverEnterSound;
    [SerializeField]
    private AudioClip _onHoldSound;

    [Header("StateVisuals")]
    [SerializeField]
    private Sprite _defaultSprite;
    [SerializeField]
    private Sprite _hoveredSprite;
    [SerializeField]
    private Sprite _holdingSprite;
    private bool _isHovered;
    private bool _isHolding;
    private System.Random _random = new System.Random();
    public UnityEvent<BasicButton, Object> OnClickEvent;
    public UnityEvent<MonoBehaviour> OnHoveredEvent;
    public UnityEvent<MonoBehaviour> OnPointerUpEvent;
    public UnityEvent<MonoBehaviour> OnPointerDownEvent;

    public void OnValidate()
    {
        _image = GetComponent<Image>();
        _image.sprite = _defaultSprite;
        _button = GetComponent<Button>();
    }

    public void OnAwake()
    {
        _button.onClick.AddListener(OnClicked);
    }

    public void OnClicked()
    {
        OnClickEvent.Invoke(this, EventData);
    }

    public void UpdateVisuals()
    {
        if (_isHolding)
        {
            _image.sprite = _holdingSprite;
        }
        else
        {
            _image.sprite = _isHovered ? _hoveredSprite : _defaultSprite;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isHolding)
        {
            OnPointerDownEvent.Invoke(this);
            if (_isHovered)
            {
                var pitch = _random.Next(65, 95) / 100f;
                AudioManager.Instance.PlaySound(this, _onHoldSound, true, pitch);
            }
        }
        _isHolding = true;
        UpdateVisuals();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isHolding)
        {
            OnPointerUpEvent.Invoke(this);
        }
        AudioManager.Instance.StopSound(this, _onHoldSound);
        _isHolding = false;
        UpdateVisuals();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHolding)
        {
            OnHoveredEvent.Invoke(this);
            AudioManager.Instance.PlaySound(this, _onHoverEnterSound, false);
        }
        _isHovered = true;
        UpdateVisuals();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovered = false;
        UpdateVisuals();
    }

}
