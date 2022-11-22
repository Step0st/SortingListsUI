using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ListEvents : MonoBehaviour
{
    public static Action pannelDragged;

    private int _startingIndex;
    private int _placingIndex;
    
    public void OnEnter(GameObject obj)
    {
        MouseData.panelHoveredOver = obj;
    }

    public void OnExit(GameObject obj)
    {
        MouseData.panelHoveredOver = null;
    }
    
    public void OnEnterList(GameObject obj)
    {
        MouseData.listMouseIsOver = obj.GetComponent<ListManager>();
    }

    public void OnExitList(GameObject obj)
    {
        MouseData.listMouseIsOver = null;
    }
   
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
        MouseData.listOfDraggedItem = obj.GetComponentInParent<ListManager>();
    }

    private GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        tempItem = Instantiate(obj,obj.transform.position,quaternion.identity);
        tempItem.transform.SetParent(transform.parent.parent);
        tempItem.GetComponent<Image>().raycastTarget = false;
        return tempItem;
    }
    
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);

        if (MouseData.listMouseIsOver != null && MouseData.listMouseIsOver == MouseData.listOfDraggedItem)
        {
            DeclareIndexes(obj);
            MovePanels(obj);
            MouseData.listMouseIsOver.database.MoveInsideList(_startingIndex-1,_placingIndex-1, 
                MouseData.listMouseIsOver.database);
        }
        if (MouseData.listMouseIsOver != null && MouseData.listMouseIsOver != MouseData.listOfDraggedItem)
        {
            DeclareIndexes(obj);
            MovePanels(obj);
            MouseData.listMouseIsOver.database.MoveBetweenLists(_startingIndex-1,_placingIndex-1, 
                MouseData.listOfDraggedItem.database, MouseData.listMouseIsOver.database);
            pannelDragged?.Invoke();
        }
    }
    
    private void DeclareIndexes(GameObject obj)
    {
        _startingIndex = obj.transform.GetSiblingIndex();
        if (MouseData.panelHoveredOver != null)
        {
            _placingIndex = MouseData.panelHoveredOver.transform.GetSiblingIndex();
        }
        else
        {
            _placingIndex = MouseData.listMouseIsOver.database.GetInfoPanels.Count+1;
        }
    }

    private void MovePanels(GameObject obj)
    {
        obj.transform.SetParent(MouseData.listMouseIsOver.container);
        obj.transform.SetSiblingIndex(_placingIndex);
    }
}