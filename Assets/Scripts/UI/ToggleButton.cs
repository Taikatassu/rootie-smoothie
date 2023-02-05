using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(WidgetSwitcher))]
public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    [SerializeField]
    private WidgetSwitcher _switcher;
    
    [Header("Debug")]
    [SerializeField]
    private bool _state = false;

    [FormerlySerializedAs("_onValueChanged")] [SerializeField]
    public UnityEvent<bool> OnValueChanged = new UnityEvent<bool>();

    public void OnValidate()
    {
        _button = GetComponent<Button>();
        _switcher = GetComponent<WidgetSwitcher>();
        SetToggleState(_state);
    }

    public void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SetToggleState(!_state);
        OnValueChanged.Invoke(_state);
    }

    public void SetToggleState(bool value)
    {
        _state = value;
        _switcher.SetActiveIndex(value ? 0 : 1);
    }
}
