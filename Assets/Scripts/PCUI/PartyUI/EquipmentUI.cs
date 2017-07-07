﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface EquipmentUIInterface
{
    void LoadEquipmentUI(CharacterModel model);
}

public class EquipmentUI : MonoBehaviour, EquipInterface
{

    public Text[] actives;
    public Text[] pasives;
    public Text[] traits;
    public Text cname, clevel, str, agi, con, wis, intl, end;
    public Text eqMain, eqOff, eqHead, eqBody, eqAcc1, eqAcc2;
    public InventoryUI invUI;
    public SkillsUI skillUI;

    CharacterModel model;
    int selectedSlot;

    // Use this for initialization
    void Start()
    {
        invUI.SetEquipImpl(this);
        invUI.gameObject.SetActive(false);
        eqMain.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqMain, EqSlot.MainHand, 0); });
        eqOff.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqOff, EqSlot.OffHand, 1); });
        eqHead.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqHead, EqSlot.Head, 2); });
        eqBody.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqBody, EqSlot.Body, 3); });
        eqAcc1.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqAcc1, EqSlot.Acc, 4); });
        eqAcc2.GetComponent<Button>().onClick.AddListener(delegate { OnEquipmentClicked(eqAcc2, EqSlot.Acc, 5); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        invUI.gameObject.SetActive(false);
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
            text.text = "None";
        else
            text.text = PlayerSession.GetEquipment(index).name;
    }

    void LoadAllAttributes()
    {
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

    }

    void SetAttributeText(Text uiText, int baseStat, int eqStat)
    {
        uiText.text = baseStat.ToString() + " + " + eqStat.ToString();
    }

    void SetActives(Text act, string activeName)
    {
        if (activeName == null || activeName.Length == 0)
        {
            act.text = "None";
        }
        else
        {
            act.text = ActiveSkillManager.GetInstance().GetActive(activeName).name;
        }
    }

    void OnEquipmentClicked(Text text, EqSlot slot, int indexSlot)
    {
        if (invUI.isActiveAndEnabled)
        {
            if (indexSlot == selectedSlot)
            {
                invUI.gameObject.SetActive(false);
                RemoveEquipment(indexSlot);
                text.text = "None";
            }
            else
            {
                selectedSlot = indexSlot;
                invUI.ChangeFilter(indexSlot);
            }
        }
        else
        {
            selectedSlot = indexSlot;
            invUI.gameObject.SetActive(true);
            invUI.ChangeFilter(indexSlot);
        }
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
        invUI.gameObject.SetActive(false);
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
}