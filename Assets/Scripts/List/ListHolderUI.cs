using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListHolderUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private ListManager _listManager;
    [SerializeField] private bool _isSortable = false;
    [SerializeField] private GameObject _sortingPanel;
    
    [HideInInspector] public SwitchToggle numberToggle;
    [HideInInspector] public SwitchToggle textToggle; 
    
    
    private void Start()
    {
        _nameText.text = _listManager.database.name;
        _countText.text = _listManager.database.GetInfoPanels.Count.ToString();
        numberToggle = _sortingPanel.transform.GetChild(0).GetComponent<SwitchToggle>();
        textToggle = _sortingPanel.transform.GetChild(1).GetComponent<SwitchToggle>();
        
        ListEvents.pannelDragged += () =>
        {
            _countText.text = _listManager.database.GetInfoPanels.Count.ToString();
        };

        if (_isSortable == true)
        {
            _sortingPanel.SetActive(true);
        }



        numberToggle.toggleSwitched += () => 
        {
            if (numberToggle._toggle.isOn)
            {
                _listManager.SortNumberDescending();
            }
            else
            {
                _listManager.SortNumberAscending();
            }
        };
        
        textToggle.toggleSwitched += () => 
        {
            if (textToggle._toggle.isOn)
            {
                _listManager.SortStringDescending();
            }
            else
            {
                _listManager.SortStringAscending();
            }
        };
    }
}
