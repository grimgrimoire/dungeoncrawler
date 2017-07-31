﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface ConsumableItemInterface{
    void OnItemClicked(int index);
}

public class ConsumableListItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Text itemName;
    public Text itemTotal;
    public Text itemType;

    ItemModel model;
    TooltipInterface tooltip;
    ConsumableItemInterface consumable;

    public void SetInterface(ConsumableItemInterface con, TooltipInterface tool)
    {
        consumable = con;
        tooltip = tool;
    }

    public void SetModel(ItemModel model, int qty)
    {
        this.model = model;
        itemName.text = model.name;
        itemType.text = model.item.ToString();
        itemTotal.text = "X "+ qty.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.ShowTooltip(model.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.HideTooltip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tooltip.HideTooltip();
        consumable.OnItemClicked(transform.GetSiblingIndex());
    }
}
