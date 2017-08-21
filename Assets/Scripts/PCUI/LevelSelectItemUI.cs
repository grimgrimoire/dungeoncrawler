using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public interface ILevelSelect
{
    void OnLvSelectClicked(int index);
}

public class LevelSelectItemUI : MonoBehaviour, IPointerClickHandler {
    public Text levelName;
    public Text levelLvReq;
    ILevelSelect iLvSelect;

    public void SetLevelInfo(List<UILevelInfo> info, int index)
    {
        levelName.text = info[index].subStageName;
        levelLvReq.text = "Recomended Level " + info[index].subStageLv;
    }

    public void SetLevelSelectListener(ILevelSelect listener)
    {
        iLvSelect = listener;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        iLvSelect.OnLvSelectClicked(transform.GetSiblingIndex());
    }

    
}
