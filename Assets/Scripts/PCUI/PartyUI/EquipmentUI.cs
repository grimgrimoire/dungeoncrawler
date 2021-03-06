﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface EquipmentUIInterface
{
    void LoadEquipmentUI(CharacterModel model);
}

public interface EquipmentUILevelUpInterface
{
    void RequestLevelUpMenu(CharacterModel model);
}

public class EquipmentUI : MonoBehaviour, EquipInterface, SetSkillInterface, IEquipedItem
{
    const string NONE = "None";
    public Text[] actives;
    public Text[] pasives;
    public Text[] traits;
    public Text cname, clevel, str, agi, con, wis, intl, end;
    public Text eqMain, eqOff, eqHead, eqBody, eqAcc1, eqAcc2;
    public InventoryUI invUI;
    public SkillsUI skillUI;
    public Button closeBtn;

    CharacterModel model;
    int selectedSlot;
    bool isPassive;
    EquipmentUILevelUpInterface levelUpInterface;
    IPopupMenu popupMenu;

    Text[] eqArray;

    // Use this for initialization
    void Start()
    {
        invUI.SetEquipImpl(this);
        invUI.gameObject.SetActive(false);
        clevel.GetComponent<Button>().onClick.AddListener(LevelUpCharacter);
        actives[0].GetComponent<Button>().onClick.AddListener(delegate { OnActiveSkillChange(0); });
        actives[1].GetComponent<Button>().onClick.AddListener(delegate { OnActiveSkillChange(1); });
        actives[2].GetComponent<Button>().onClick.AddListener(delegate { OnActiveSkillChange(2); });
        actives[3].GetComponent<Button>().onClick.AddListener(delegate { OnActiveSkillChange(3); });
        actives[4].GetComponent<Button>().onClick.AddListener(delegate { OnActiveSkillChange(4); });
        closeBtn.onClick.AddListener(CloseSelection);
        eqArray = new Text[6] { eqMain, eqOff, eqHead, eqBody, eqAcc1, eqAcc2 };
    }

    public void SetInterfaces(MainMenuUI iis)
    {
        invUI.SetInterfaces(iis);
        eqMain.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(0);
        eqOff.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(1);
        eqHead.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(2);
        eqBody.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(3);
        eqAcc1.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(4);
        eqAcc2.GetComponent<EquipedItemUI>().SetInterface(iis, this).SetEquipIndex(5);
    }

    void OnDisable()
    {
        closeBtn.gameObject.SetActive(false);
        invUI.ClearItemList();
        invUI.gameObject.SetActive(false);
        skillUI.ClearList();
        skillUI.gameObject.SetActive(false);
    }

    public void SetLevelUpInterface(MainMenuUI mainmenu)
    {
        levelUpInterface = mainmenu;
        popupMenu = mainmenu;
        skillUI.SetInterface(this, mainmenu);
        for (int i = 0; i < 5; i++)
            actives[i].GetComponent<EquipedSkillUI>().SetInterface(mainmenu);
    }

    void LevelUpCharacter()
    {
        if (model.levelUp)
            levelUpInterface.RequestLevelUpMenu(model);
    }

    public void CloseSelection()
    {
        popupMenu.CancelPopUpMenu();
        invUI.ClearItemList();
        invUI.gameObject.SetActive(false);
        skillUI.ClearList();
        skillUI.gameObject.SetActive(false);
        closeBtn.gameObject.SetActive(false);
    }

    public void SetCharacterModel(CharacterModel model)
    {
        this.model = model;
        cname.text = model.name;
        LoadAllEquipments();
        LoadAllAttributes();
        LoadAllActives();
        LoadAllPassives();
    }

    void LoadAllEquipments()
    {
        LoadEquipmentName(eqMain, model.battleSetting.mainHand);
        LoadEquipmentName(eqOff, model.battleSetting.offHand);
        LoadEquipmentName(eqHead, model.battleSetting.head);
        LoadEquipmentName(eqBody, model.battleSetting.body);
        LoadEquipmentName(eqAcc1, model.battleSetting.acc1);
        LoadEquipmentName(eqAcc2, model.battleSetting.acc2);
    }

    void LoadEquipmentName(Text text, int index)
    {
        if (index < 0)
            text.text = NONE;
        else
            text.text = PlayerSession.GetEquipment(index).name;
        text.GetComponent<EquipedItemUI>().SetModel(PlayerSession.GetEquipment(index));
    }

    public void LoadAllAttributes()
    {
        clevel.text = "Level " + model.level;
        if (model.levelUp)
            clevel.text += "\nLevel UP!";
        model.CalculateEqAttribute();
        SetAttributeText(str, model.attribute.str, model.eqAttribute.str);
        SetAttributeText(agi, model.attribute.agi, model.eqAttribute.agi);
        SetAttributeText(con, model.attribute.cons, model.eqAttribute.cons);
        SetAttributeText(wis, model.attribute.wisdom, model.eqAttribute.wisdom);
        SetAttributeText(intl, model.attribute.intel, model.eqAttribute.intel);
        SetAttributeText(end, model.attribute.endurance, model.eqAttribute.endurance);
    }

    void LoadAllActives()
    {
        int i = 0;
        foreach (string actId in model.actives)
        {
            SetActives(actives[i], actId);
            i++;
        }
        i = 0;
    }

    void LoadAllPassives()
    {
        //TODO TO be implemented
    }

    void SetAttributeText(Text uiText, int baseStat, int eqStat)
    {
        uiText.text = baseStat.ToString() + " + " + eqStat.ToString();
    }

    void SetActives(Text act, string activeName)
    {
        if (activeName == null || activeName.Length == 0)
        {
            act.text = NONE;
            act.GetComponent<EquipedSkillUI>().SetDescription("No active skill");
        }
        else
        {
            act.text = ActiveSkillManager.GetInstance().GetActive(activeName).name;
            act.GetComponent<EquipedSkillUI>().SetDescription(ActiveSkillManager.GetInstance().GetActive(activeName).info);
        }
    }

    void ShowEquipmentSelection(int indexSlot)
    {
        popupMenu.CancelPopUpMenu();
        closeBtn.gameObject.SetActive(true);
        skillUI.gameObject.SetActive(false);
        invUI.gameObject.SetActive(true);
        invUI.ChangeFilter(indexSlot);
    }

    public void OnItemEquiped(int index)
    {
        switch (selectedSlot)
        {
            case 0:
                Unequip(model.battleSetting.mainHand);
                model.battleSetting.mainHand = index;
                break;
            case 1:
                Unequip(model.battleSetting.offHand);
                model.battleSetting.offHand = index;
                break;
            case 2:
                Unequip(model.battleSetting.head);
                model.battleSetting.head = index;
                break;
            case 3:
                Unequip(model.battleSetting.body);
                model.battleSetting.body = index;
                break;
            case 4:
                Unequip(model.battleSetting.acc1);
                model.battleSetting.acc1 = index;
                break;
            case 5:
                Unequip(model.battleSetting.acc2);
                model.battleSetting.acc2 = index;
                break;
        }
        PlayerSession.GetEquipment(index).isUsed = true;
        LoadAllEquipments();
        LoadAllAttributes();
        CloseSelection();
    }

    void RemoveEquipment(int slotIndex)
    {
        switch (slotIndex)
        {
            case 0:
                Unequip(model.battleSetting.mainHand);
                model.battleSetting.mainHand = -1;
                break;
            case 1:
                Unequip(model.battleSetting.offHand);
                model.battleSetting.offHand = -1;
                break;
            case 2:
                Unequip(model.battleSetting.head);
                model.battleSetting.head = -1;
                break;
            case 3:
                Unequip(model.battleSetting.body);
                model.battleSetting.body = -1;
                break;
            case 4:
                Unequip(model.battleSetting.acc1);
                model.battleSetting.acc1 = -1;
                break;
            case 5:
                Unequip(model.battleSetting.acc2);
                model.battleSetting.acc2 = -1;
                break;
        }
    }

    void Unequip(int index)
    {
        if (index > -1)
            PlayerSession.GetEquipment(index).isUsed = false;
    }

    void OnActiveSkillChange(int selIndex)
    {
        selectedSlot = selIndex;
        isPassive = false;
        if (!skillUI.isActiveAndEnabled)
        {
            ShowSkillSelection();
        }
        else
        {
            CloseSelection();
            EquipSelectedActives("");
        }

    }

    void ShowSkillSelection()
    {
        popupMenu.CancelPopUpMenu();
        closeBtn.gameObject.SetActive(true);
        invUI.gameObject.SetActive(false);
        skillUI.gameObject.SetActive(true);
        skillUI.LoadAllAvailableActives(model.learnActive, model.actives);
    }

    public void EquipSelectedSkill(string id)
    {
        if (isPassive)
        {

        }
        else
        {
            EquipSelectedActives(id);
            CloseSelection();
        }

    }

    void EquipSelectedActives(string id)
    {
        model.actives[selectedSlot] = id;
        SetActives(actives[selectedSlot], id);
    }

    public void OnRequestPopupMenu(int index)
    {
        if (eqArray[index].text.Equals(NONE))
            return;
        selectedSlot = index;
        popupMenu.CreatePopUpMenu(new string[] { "Unequip" }, new UnityEngine.Events.UnityAction[] { Unequip });
    }

    void Unequip()
    {
        CloseSelection();
        RemoveEquipment(selectedSlot);
        eqArray[selectedSlot].text = NONE;
        LoadAllAttributes();
    }

    public void OnEquipmentClicked(int index)
    {
        if (invUI.isActiveAndEnabled)
        {
            if (index != selectedSlot)
            {
                selectedSlot = index;
                invUI.ChangeFilter(index);
            }
        }
        else
        {
            selectedSlot = index;
            ShowEquipmentSelection(index);
        }
    }
}
