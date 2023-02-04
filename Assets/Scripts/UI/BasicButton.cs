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
    [SerializeField]
    private AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
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
                _audioSource.clip = _onHoldSound;
                _audioSource.pitch = _random.Next(65, 95) / 100f;
                _audioSource.loop = true;
                _audioSource.Play();
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
            _audioSource.Stop();
        }
        _isHolding = false;
        UpdateVisuals();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isHolding)
        {
            OnHoveredEvent.Invoke(this);
            _audioSource.clip = _onHoverEnterSound;
            _audioSource.loop = false;
            _audioSource.Play();
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
