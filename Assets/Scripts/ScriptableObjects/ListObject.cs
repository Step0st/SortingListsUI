using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New List", menuName = "UI Lists/Create new list")]
public class ListObject : ScriptableObject
{
    public InfoPanelsList container;
    public List<InfoPanel> GetInfoPanels 
    { 
        get => container.InfoPanels;
        set => container.InfoPanels = value;
    }
    
    public void MoveInsideList(int oldIndex, int newIndex, ListObject list)
    {
        InfoPanel item = list.GetInfoPanels[oldIndex];
        list.GetInfoPanels.RemoveAt(oldIndex);
        if (newIndex <= list.GetInfoPanels.Count)
        {
            list.GetInfoPanels.Insert(newIndex, item);
        }
        else
        {
            list.GetInfoPanels.Add(item);
        }
    }
    
    public void MoveBetweenLists(int oldIndex, int newIndex, ListObject oldList, ListObject newList)
    {
        InfoPanel item = oldList.GetInfoPanels[oldIndex];
        oldList.GetInfoPanels.RemoveAt(oldIndex);
        if (newIndex <= newList.GetInfoPanels.Count)
        {
            newList.GetInfoPanels.Insert(newIndex, item);
        }
        else
        {
            newList.GetInfoPanels.Add(item);
        }
    }
}
