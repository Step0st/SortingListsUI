using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ListManager : MonoBehaviour
{
   public ListObject database;
   [HideInInspector] public Transform container;
   
   private ListEvents _listEvents;
   [SerializeField] private GameObject _panelTemplate;
   
   private void OnApplicationQuit()
   {
      SaveSystem.SaveData(database);
   }

   private void Start()
   {
      _listEvents = GetComponent<ListEvents>();
      database.GetInfoPanels = SaveSystem.LoadData(database).GetInfoPanels;
      container = _panelTemplate.transform.parent.transform;
      InitializeList();
      _panelTemplate.SetActive(false);
      
      AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { _listEvents.OnEnterList(gameObject); });
      AddEvent(gameObject, EventTriggerType.PointerExit, delegate { _listEvents.OnExitList(gameObject); });
   }
   
   private void InitializeList()
   {
      for (int i = 0; i < database.GetInfoPanels.Count; i++)
      {
         var panel = Instantiate(_panelTemplate, container);
         var numHolder = panel.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
         var infoHolder = panel.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
         numHolder.text = database.GetInfoPanels[i].number.ToString();
         infoHolder.text = database.GetInfoPanels[i].description;

         AddEvent(panel, EventTriggerType.PointerEnter, delegate { _listEvents.OnEnter(panel); });
         AddEvent(panel, EventTriggerType.PointerExit, delegate { _listEvents.OnExit(panel); });
         AddEvent(panel, EventTriggerType.BeginDrag, delegate { _listEvents.OnDragStart(panel); });
         AddEvent(panel, EventTriggerType.EndDrag, delegate { _listEvents.OnDragEnd(panel); });
         AddEvent(panel, EventTriggerType.Drag, delegate { _listEvents.OnDrag(panel); });
      }
   }
   
   protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
   {
      EventTrigger trigger = obj.GetComponent<EventTrigger>();
      var eventTrigger = new EventTrigger.Entry();
      eventTrigger.eventID = type;
      eventTrigger.callback.AddListener(action);
      trigger.triggers.Add(eventTrigger);
   }

   public void SortNumberAscending()
   {
      database.GetInfoPanels.Sort((s1,s2) => s1.number.CompareTo(s2.number));
      ReassignValues();
   }
   
   public void SortNumberDescending()
   {
      database.GetInfoPanels.Sort((s1,s2) => s2.number.CompareTo(s1.number));
      ReassignValues();
   }
   
   public void SortStringAscending()
   {
      database.GetInfoPanels.Sort((s1,s2) => s1.description.CompareTo(s2.description));
      ReassignValues();
   }
   
   public void SortStringDescending()
   {
      database.GetInfoPanels.Sort((s1,s2) => s2.description.CompareTo(s1.description));
      ReassignValues();
   }
   
   private void ReassignValues()
   {
      for (int i = 0; i < database.GetInfoPanels.Count; i++)
      {
         var numHolder = container.GetChild(i+1).transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
         var infoHolder = container.GetChild(i+1).transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
         numHolder.text = database.GetInfoPanels[i].number.ToString();
         infoHolder.text = database.GetInfoPanels[i].description;
      }
   }
}
