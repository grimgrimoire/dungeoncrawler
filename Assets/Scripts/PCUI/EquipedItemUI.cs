using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IEquipedItem
{
    void OnRequestPopupMenu(int index);
    void OnEquipmentClicked(int index);
}

public class EquipedItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    InventoryItemShowInterface iisImpl;
    IEquipedItem iEquip;
    Equipment model;
    int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public EquipedItemUI SetInterface(InventoryItemShowInterface iis, IEquipedItem ieq)
    {
        iisImpl = iis;
        iEquip = ieq;
        return this;
    }

    public void SetEquipIndex(int index)
    {
        this.index = index;
    }

    public void SetModel(Equipment model)
    {
        this.model = model;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (iisImpl != null)
            iisImpl.OnCloseEquipmentStatus();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (iisImpl != null && model != null)
            iisImpl.OnShowEquipmentStatus(model, eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            iEquip.OnRequestPopupMenu(transform.GetSiblingIndex());
            iisImpl.OnCloseEquipmentStatus();
        }
        else
            iEquip.OnEquipmentClicked(transform.GetSiblingIndex());
    }
}
