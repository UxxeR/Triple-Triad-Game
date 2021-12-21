using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance { get; set; }
    [field: SerializeField] public ListSettingUI<Resolution> ResolutionSetting { get; set; }
    [field: SerializeField] public ListSettingUI<string> QualitySetting { get; set; }
    [field: SerializeField] public VolumeSettingUI MusicSetting { get; set; }
    [field: SerializeField] public VolumeSettingUI SFXSetting { get; set; }
    [field: SerializeField] public Toggle FullScreen { get; set; }
    [field: SerializeField] public Toggle Vsync { get; set; }
    [field: SerializeField] public Toggle SameRule { get; set; }
    [field: SerializeField] public Toggle SameWallRule { get; set; }
    [field: SerializeField] public Toggle SuddenDeathRule { get; set; }
    [field: SerializeField] public Toggle RandomRule { get; set; }
    [field: SerializeField] public Toggle PlusRule { get; set; }
    [field: SerializeField] public Toggle ElementalRule { get; set; }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        ResolutionSetting.Values = Screen.resolutions.ToList();
        QualitySetting.Values = QualitySettings.names.ToList();
        ResolutionSetting.AddButton.onClick.AddListener(() => ResolutionSetting.ModifyIndex(1));
        ResolutionSetting.SubtractButton.onClick.AddListener(() => ResolutionSetting.ModifyIndex(-1));
        QualitySetting.AddButton.onClick.AddListener(() => QualitySetting.ModifyIndex(1));
        QualitySetting.SubtractButton.onClick.AddListener(() => QualitySetting.ModifyIndex(-1));
        BindData();
        Refresh();
        SetSettings();
    }

    /// <summary>
    /// Save the current settings in a new file or override an existing file.
    /// </summary>
    public void SaveSettings()
    {
        SetSettings();
        DataController.Instance.Settings = new SettingsData(MusicSetting.Slider.value,
                                        SFXSetting.Slider.value,
                                        ResolutionSetting.CurrentIndex,
                                        QualitySetting.CurrentIndex,
                                        FullScreen.isOn,
                                        Vsync.isOn,
                                        SameRule.isOn,
                                        SameWallRule.isOn,
                                        SuddenDeathRule.isOn,
                                        RandomRule.isOn,
                                        PlusRule.isOn,
                                        ElementalRule.isOn);
        DataSerialization.Save("Settings", DataController.Instance.Settings);
    }

    /// <summary>
    /// Set the current graphic settings.
    /// </summary>
    private void SetSettings()
    {
        SetQuality(QualitySetting.CurrentIndex);
        SetResolution(ResolutionSetting.Values[ResolutionSetting.CurrentIndex]);
        SetFullScreen(FullScreen.isOn);
        SetVsync(Vsync.isOn);
    }

    /// <summary>
    /// Cancel the current settings and return to the saved settings.
    /// </summary>
    public void CancelSettings()
    {
        BindData();
        Refresh();
    }

    /// <summary>
    /// Bind the current settings with the UI elements.
    /// </summary>
    private void BindData()
    {
        this.ResolutionSetting.CurrentIndex = DataController.Instance.Settings.ResolutionIndex;
        this.QualitySetting.CurrentIndex = DataController.Instance.Settings.QualityIndex;
        this.MusicSetting.Slider.value = DataController.Instance.Settings.MusicVolume;
        this.SFXSetting.Slider.value = DataController.Instance.Settings.SFXVolume;
        this.FullScreen.isOn = DataController.Instance.Settings.FullScreen;
        this.Vsync.isOn = DataController.Instance.Settings.Vsync;
        this.SameRule.isOn = DataController.Instance.Settings.SameRule;
        this.SameWallRule.isOn = DataController.Instance.Settings.SameWallRule;
        this.SuddenDeathRule.isOn = DataController.Instance.Settings.SuddenDeathRule;
        this.RandomRule.isOn = DataController.Instance.Settings.RandomRule;
        this.PlusRule.isOn = DataController.Instance.Settings.PlusRule;
        this.ElementalRule.isOn = DataController.Instance.Settings.ElementalRule;
    }

    /// <summary>
    /// Refresh the current settings with the UI elements.
    /// </summary>
    public void Refresh()
    {
        this.MusicSetting.UpdateValue();
        this.SFXSetting.UpdateValue();
        this.ResolutionSetting.UpdateValue();
        this.QualitySetting.UpdateValue();
    }

    /// <summary>
    /// Enable or disable the fullscreen.
    /// </summary>
    /// <param name="isFullScreen">Fullscreen mode.</param>
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    /// <summary>
    /// Change the quality of the game.
    /// </summary>
    /// <param name="qualityIndex">Quality index.</param>
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Change the resolution of the game.
    /// </summary>
    /// <param name="resolution">The resolution.</param>
    public void SetResolution(Resolution resolution)
    {
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /// <summary>
    /// Enable or disable the Vsync.
    /// </summary>
    /// <param name="isVsync">Vsync mode.</param>
    public void SetVsync(bool isVsync)
    {
        if (isVsync)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
