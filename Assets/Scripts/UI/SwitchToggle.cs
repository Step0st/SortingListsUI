using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] private RectTransform _handelRT;
    private Vector2 _handlePosition;
    
    [HideInInspector] public Toggle _toggle;
    public Action toggleSwitched;
    
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _handlePosition = _handelRT.anchoredPosition;
        
        _toggle.onValueChanged.AddListener(OnSwitch);
        if (_toggle.isOn)
        {
            OnSwitch(true);
        }
    }

    private void OnSwitch(bool on)
    {
        _handelRT.anchoredPosition = on ? _handlePosition * -1 : _handlePosition;
        toggleSwitched?.Invoke();
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnSwitch);
    }
}
