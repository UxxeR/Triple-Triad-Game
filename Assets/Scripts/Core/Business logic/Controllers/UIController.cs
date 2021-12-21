using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup currentSettingTab;
    public void ChangeSettingsTab(CanvasGroup settingWindow)
    {
        if (currentSettingTab != null)
        {
            currentSettingTab.alpha = 0f;
            currentSettingTab.blocksRaycasts = false;
        }

        currentSettingTab = settingWindow;
        settingWindow.alpha = 1f;
        settingWindow.blocksRaycasts = true;
    }

    public void ShowWindow(CanvasGroup window)
    {
        window.alpha = window.alpha < 1f ? 1f : 0f;
        window.blocksRaycasts = window.blocksRaycasts ? false : true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
