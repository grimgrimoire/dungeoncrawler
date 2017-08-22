using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class DungeonSelectUI : MonoBehaviour, IDungeonSelect, ILevelSelect
{
    public GameObject dungeonList;
    public GameObject levelList;
    public GameObject dungeonPrefab;
    public GameObject levelPrefab;
    public Button backButton;
    int selectedDungeonIndex;

    // Use this for initialization
    void Start()
    {
        DungeonSelectManager.GetInstance();
        InitializeListener();
        UpdateDungeonList();
    }

    void InitializeListener()
    {
        foreach(Transform dungeon in dungeonList.transform)
        {
            dungeon.GetComponent<DungeonSelectItemUI>().SetDungeonSelectListener(this);
        }

        foreach(Transform level in levelList.transform)
        {
            level.GetComponent<LevelSelectItemUI>().SetLevelSelectListener(this);
        }
        backButton.onClick.AddListener(BackAction);
    }

    public void ShowDungeonList()
    {
        dungeonList.SetActive(true);
        levelList.SetActive(false);
        backButton.gameObject.SetActive(false);
        HideAllLevelItem();
    }

    public void ShowLevelList(UIDungeonInfo info, int index)
    {
        dungeonList.SetActive(false);
        levelList.SetActive(true);
        backButton.gameObject.SetActive(true);
        UpdateLevelList(info, index);
    }

    public void BackAction()
    {
        ShowDungeonList();
    }

    public void OnDungeonSelected(int index)
    {
        ShowLevelList(DungeonSelectManager.GetInstance().GetModel().stage[index], index);
        selectedDungeonIndex = index;
    }

    public void UpdateDungeonList()
    {
        int totalItem = DungeonSelectManager.GetInstance().GetModel().stage.Count;
        for (int i = 0; i < totalItem; i++)
        {
            if (i < dungeonList.transform.childCount)
                UpdateDungeonItem(i, DungeonSelectManager.GetInstance().GetModel().stage[i]);
            else
                CreateDungeonItem(DungeonSelectManager.GetInstance().GetModel().stage[i]);
        }
    }

    void HideAllDungeonItem()
    {
        foreach (Transform child in dungeonList.transform)
            child.gameObject.SetActive(false);
    }

    void CreateDungeonItem(UIDungeonInfo info)
    {
        GameObject dungeon = Instantiate(dungeonPrefab, dungeonList.transform, false);
        dungeon.GetComponent<DungeonSelectItemUI>().SetDungeonInfo(info);
        dungeon.GetComponent<DungeonSelectItemUI>().SetDungeonSelectListener(this);
    }

    void UpdateDungeonItem(int index, UIDungeonInfo info)
    {
        GameObject dungeon = dungeonList.transform.GetChild(index).gameObject;
        dungeon.GetComponent<DungeonSelectItemUI>().SetDungeonInfo(info);
        dungeon.SetActive(true);
    }

    void UpdateLevelList(UIDungeonInfo info, int index)
    {
        int totalLevel = DungeonSelectManager.GetInstance().GetStageLevels(index).Count;
        for (int i=0;i<totalLevel;i++)
        {
            if (i < levelList.transform.childCount)
                UpdateLevelItem(i, DungeonSelectManager.GetInstance().GetStageLevels(index));
            else
                CreateLevelItem(i,DungeonSelectManager.GetInstance().GetStageLevels(index));
        }
    }

    void HideAllLevelItem()
    {
        foreach (Transform child in levelList.transform)
            child.gameObject.SetActive(false);
    }

    void CreateLevelItem(int index, List<UILevelInfo> info)
    {
        GameObject level = Instantiate(levelPrefab, levelList.transform, false);
        level.GetComponent<LevelSelectItemUI>().SetLevelInfo(info, index);
        level.GetComponent<LevelSelectItemUI>().SetLevelSelectListener(this);
        
    }

    void UpdateLevelItem(int index, List<UILevelInfo> info)
    {
        GameObject level = levelList.transform.GetChild(index).gameObject;
        level.GetComponent<LevelSelectItemUI>().SetLevelInfo(info, index);
        level.SetActive(true);
    }

    public void OnLevelSelected(int index)
    {
        PlayerSession.GetProfile().currentDungeonId = DungeonSelectManager.GetInstance().GetStageLevels(selectedDungeonIndex)[index].subStageId;
        SceneManager.LoadScene(1);
    }

    public void OnDungeonSelectClicked(int index)
    {
        OnDungeonSelected(index);
    }

    public void OnLvSelectClicked(int index)
    {
        OnLevelSelected(index);
    }
}
