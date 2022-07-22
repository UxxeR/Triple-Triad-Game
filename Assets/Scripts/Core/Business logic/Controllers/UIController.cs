using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    [SerializeField] private CanvasGroup currentSettingTab;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Open a tab and change the information showed.
    /// </summary>
    /// <param name="settingWindow">Information window.</param>
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

    /// <summary>
    /// Show a new window UI.
    /// </summary>
    /// <param name="window">The CanvasGroup component from the window that will be shown.</param>
    public void ShowWindow(CanvasGroup window)
    {
        window.alpha = window.alpha < 1f ? 1f : 0f;
        window.blocksRaycasts = window.blocksRaycasts ? false : true;
    }
}
