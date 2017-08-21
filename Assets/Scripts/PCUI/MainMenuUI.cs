﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public interface TooltipInterface
{
    void ShowTooltip(string text);
    void HideTooltip();
}

public interface ConfirmationDialogInterface
{
    void RequestConfirmationDialog(string text, UnityAction onYes, UnityAction onNo, UnityAction onCancel);
}

public class MainMenuUI : MonoBehaviour, CharCreationInterface, RecruitmentInterface, EquipmentUIInterface, EquipmentUILevelUpInterface, InventoryItemShowInterface, TooltipInterface, ConsumableInterface, ConfirmationDialogInterface, IPopupMenu, IPointerClickHandler
{
    public DungeonSelectUI dungeonSelect;
    public CharCreationUI charCreate;
    public PartyManager partyManager;
    public RecruitmentManager recruitmentManager;
    public EquipmentUI equipmentUI;
    public InventoryUI inventoryUI;
    public ConsumableItemUI consumableUI;
    public RectTransform equipmentShowPopUp, tooltip;
    public ConfirmationDialog dialog;
    public PopupMenu popupMenu;

    public void OnCreateNewChar()
    {
        partyManager.RequestUpdateMember();
        ShowPartyWindow();
    }

    // Use this for initialization
    void Start()
    {
        charCreate.SetListener(this);
        recruitmentManager.SetListener(this);
        partyManager.SetEquipmentImpl(this);
        inventoryUI.SetInterfaces(this);
        equipmentUI.SetInterfaces(this);
        equipmentUI.SetLevelUpInterface(this);
        consumableUI.SetListener(this);
        ItemManager.GetInstance();
        LoadGameSession();
        InitializeUIClasses();
    }

    void InitializeUIClasses()
    {
        popupMenu.gameObject.SetActive(true);
    }

    void LoadGameSession()
    {
        if (PlayerSession.GetInstance().IsSessionAvailable())
        {
            ContinueGameSession();
        }
        else
        {
            if (PlayerSession.GetInstance().LoadSession())
            {
                CreateNewSession();
            }
            else
            {
                ContinueGameSession();
            }
        }
    }

    void CreateNewSession()
    {
        charCreate.gameObject.SetActive(true);
        charCreate.SetForNewGame();
    }

    void ContinueGameSession()
    {

        //ADD on dungeon clear condition
        partyManager.RequestUpdateMember();
        ShowPartyWindow();
    }

    void Update()
    {
        if (equipmentShowPopUp.gameObject.activeInHierarchy)
        {
            UpdateEquipmentShowPos();
        }
        if (tooltip.gameObject.activeInHierarchy)
        {
            UpdateTooltipShowPos();
        }
    }

    void UpdateEquipmentShowPos()
    {
        float height = equipmentShowPopUp.rect.height;
        float width = equipmentShowPopUp.rect.width;
        Vector3 newPos = Input.mousePosition;
        if (newPos.y - height < 0)
        {
            newPos.y += height - newPos.y;
        }
        if (newPos.x + width > Screen.width)
        {
            newPos.x -= (newPos.x + width) - Screen.width;
        }
        equipmentShowPopUp.transform.position = newPos;
    }

    void UpdateTooltipShowPos()
    {
        float height = tooltip.rect.height;
        float width = tooltip.rect.width;
        Vector3 newPos = Input.mousePosition;
        if (newPos.y - height < 0)
        {
            newPos.y += height - newPos.y;
        }
        if (newPos.x + width > Screen.width)
        {
            newPos.x -= (newPos.x + width) - Screen.width;
        }
        tooltip.transform.position = newPos;
    }

    public void StartMockupBattle()
    {
        ShowDungeonSelectMenu();
        //InitializeBattleMembers();
        //SceneManager.LoadScene(1);
    }

    void InitializeBattleMembers()
    {
        PlayerSession.GetProfile().characters[0].GenerateBasicBattleAttribute();
        if (PlayerSession.GetProfile().party.member1 > 0)
        {
            PlayerSession.GetProfile().characters[PlayerSession.GetProfile().party.member1].GenerateBasicBattleAttribute();
        }
        if (PlayerSession.GetProfile().party.member2 > 0)
        {
            PlayerSession.GetProfile().characters[PlayerSession.GetProfile().party.member2].GenerateBasicBattleAttribute();
        }
        if (PlayerSession.GetProfile().party.member3 > 0)
        {
            PlayerSession.GetProfile().characters[PlayerSession.GetProfile().party.member3].GenerateBasicBattleAttribute();
        }
    }

    public void ShowRecruitWindow()
    {
        HideAllWindow();
        recruitmentManager.gameObject.SetActive(true);
    }

    public void ShowPartyWindow()
    {
        HideAllWindow();
        partyManager.gameObject.SetActive(true);
        PlayerSession.GetInstance().SaveSession();
    }

    public void ShowDungeonSelectMenu()
    {
        HideAllWindow();
        dungeonSelect.gameObject.SetActive(true);
    }

    public void ShowOwnedItems()
    {
        HideAllWindow();
        consumableUI.gameObject.SetActive(true);
        consumableUI.ChangeFilter(consumableUI.filter.value);
    }

    public void ShowInventory()
    {
        HideAllWindow();
        inventoryUI.gameObject.SetActive(true);
        inventoryUI.ChangeFilter(inventoryUI.slotFilter.value);
    }

    void HideAllWindow()
    {
        popupMenu.gameObject.SetActive(false);
        recruitmentManager.gameObject.SetActive(false);
        charCreate.gameObject.SetActive(false);
        partyManager.gameObject.SetActive(false);
        equipmentUI.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
        consumableUI.gameObject.SetActive(false);
        dungeonSelect.gameObject.SetActive(false);
    }

    void LevelUpCharacter(CharacterModel model)
    {
        charCreate.gameObject.SetActive(true);
        charCreate.SetForLevelUp(model);
    }

    public void OnCharLevelUp()
    {
        partyManager.RequestUpdateMember();
        if (equipmentUI.gameObject.activeInHierarchy)
            equipmentUI.LoadAllAttributes();
    }

    public void RecruitNewCharacter(CharacterModel model)
    {
        LevelUpCharacter(model);
    }

    public void LoadEquipmentUI(CharacterModel model)
    {
        equipmentUI.SetCharacterModel(model);
        HideAllWindow();
        equipmentUI.gameObject.SetActive(true);
    }

    public void OnShowEquipmentStatus(Equipment model, Vector2 pos)
    {
        equipmentShowPopUp.gameObject.SetActive(true);
        equipmentShowPopUp.GetComponent<EquipmentShowUI>().setModel(model);
    }

    public void OnCloseEquipmentStatus()
    {
        equipmentShowPopUp.gameObject.SetActive(false);
    }

    public void RequestLevelUpMenu(CharacterModel model)
    {
        LevelUpCharacter(model);
    }

    public void ShowTooltip(string text)
    {
        tooltip.GetComponent<TooltipUI>().SetText(text);
        tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    public void RequestConfirmationDialog(string text, UnityAction onYes, UnityAction onNo, UnityAction onCancel)
    {
        dialog.SetYesClicked(onYes);
        dialog.SetNoClicked(onNo);
        dialog.SetText(text);
        dialog.gameObject.SetActive(true);
    }

    public void CreatePopUpMenu(string[] texts, UnityAction[] actions)
    {
        popupMenu.gameObject.SetActive(true);
        popupMenu.AddPopupMenuButtons(texts, actions);
        UpdatePopupMenuPos();
    }

    void UpdatePopupMenuPos()
    {
        float height = tooltip.rect.height;
        float width = tooltip.rect.width;
        Vector3 newPos = Input.mousePosition;
        if (newPos.y - height < 0)
        {
            newPos.y += height - newPos.y;
        }
        if (newPos.x + width > Screen.width)
        {
            newPos.x -= (newPos.x + width) - Screen.width;
        }
        popupMenu.transform.position = newPos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(popupMenu.gameObject.activeInHierarchy)
            popupMenu.gameObject.SetActive(false);
        if (dialog.gameObject.activeInHierarchy)
            dialog.gameObject.SetActive(false);
    }

    public void CancelPopUpMenu()
    {
        popupMenu.gameObject.SetActive(false);
    }
}
