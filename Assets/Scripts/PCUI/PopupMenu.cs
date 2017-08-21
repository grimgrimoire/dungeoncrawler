using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IPopupMenu
{
    void CreatePopUpMenu(string[] texts, UnityAction[] actions);
    void CancelPopUpMenu();
}

public class PopupMenu : MonoBehaviour
{

    Button[] buttons;

    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        ClosePopupMenu();
    }

    public void AddPopupMenuButtons(string[] text, UnityAction[] action)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (i >= buttons.Length)
                break;
            SetButton(i, text[i], action[i]);
        }
    }

    void SetButton(int index, string text, UnityAction action)
    {
        buttons[index].gameObject.SetActive(true);
        buttons[index].GetComponentInChildren<Text>().text = text;
        buttons[index].onClick.AddListener(action);
        buttons[index].onClick.AddListener(ClosePopupMenu);
    }

    void OnDisable()
    {
        ClearAllListener();
    }

    void ClosePopupMenu()
    {
        gameObject.SetActive(false);
    }

    void ClearAllListener()
    {
        foreach (Button butt in buttons)
        {
            butt.onClick.RemoveAllListeners();
            butt.gameObject.SetActive(false);
        }
    }
}
