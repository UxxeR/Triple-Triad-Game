using UnityEngine;

[System.Serializable]
public class SettingsData : IData<SettingsData>
{
    [field: SerializeField] public float MusicVolume { get; set; }
    [field: SerializeField] public float SFXVolume { get; set; }
    [field: SerializeField] public int ResolutionIndex { get; set; }
    [field: SerializeField] public int QualityIndex { get; set; }
    [field: SerializeField] public bool FullScreen { get; set; }
    [field: SerializeField] public bool Vsync { get; set; }
    [field: SerializeField] public bool SameRule { get; set; }
    [field: SerializeField] public bool SameWallRule { get; set; }
    [field: SerializeField] public bool SuddenDeathRule { get; set; }
    [field: SerializeField] public bool RandomRule { get; set; }
    [field: SerializeField] public bool PlusRule { get; set; }
    [field: SerializeField] public bool ElementalRule { get; set; }

    /// <summary>
    /// Default constructor of the class.
    /// </summary>
    public SettingsData()
    {
        MusicVolume = 100f;
        SFXVolume = 100f;
        ResolutionIndex = 0;
        QualityIndex = 0;
        FullScreen = true;
        Vsync = true;
        SameRule = true;
        SameWallRule = true;
        SuddenDeathRule = true;
        RandomRule = true;
        PlusRule = true;
        ElementalRule = true;
    }

    /// <summary>
    /// Custom constructor of the class.
    /// </summary>
    public SettingsData(float MusicVolume,
                        float SFXVolume,
                        int ResolutionIndex,
                        int QualityIndex,
                        bool FullScreen,
                        bool Vsync,
                        bool SameRule,
                        bool SameWallRule,
                        bool SuddenDeathRule,
                        bool RandomRule,
                        bool PlusRule,
                        bool ElementalRule)
    {
        this.MusicVolume = MusicVolume;
        this.SFXVolume = SFXVolume;
        this.ResolutionIndex = ResolutionIndex;
        this.QualityIndex = QualityIndex;
        this.FullScreen = FullScreen;
        this.Vsync = Vsync;
        this.SameRule = SameRule;
        this.SameWallRule = SameWallRule;
        this.SuddenDeathRule = SuddenDeathRule;
        this.RandomRule = RandomRule;
        this.PlusRule = PlusRule;
        this.ElementalRule = ElementalRule;
    }

    /// <summary>
    /// Get the settings data of the current UI elements.
    /// </summary>
    /// <returns>The settings data.</returns>
    public SettingsData GetData()
    {
        MusicVolume = SettingsController.Instance.MusicSetting.Slider.value;
        SFXVolume = SettingsController.Instance.SFXSetting.Slider.value;
        ResolutionIndex = SettingsController.Instance.ResolutionSetting.CurrentIndex;
        QualityIndex = SettingsController.Instance.QualitySetting.CurrentIndex;
        FullScreen = SettingsController.Instance.FullScreen.isOn;
        Vsync = SettingsController.Instance.Vsync.isOn;
        SameRule = SettingsController.Instance.SameRule.isOn;
        SameWallRule = SettingsController.Instance.SameWallRule.isOn;
        SuddenDeathRule = SettingsController.Instance.SuddenDeathRule.isOn;
        RandomRule = SettingsController.Instance.RandomRule.isOn;
        PlusRule = SettingsController.Instance.PlusRule.isOn;
        ElementalRule = SettingsController.Instance.ElementalRule.isOn;
        return new SettingsData(MusicVolume,
                                SFXVolume,
                                ResolutionIndex,
                                QualityIndex,
                                FullScreen,
                                Vsync,
                                SameRule,
                                SameWallRule,
                                SuddenDeathRule,
                                RandomRule,
                                PlusRule,
                                ElementalRule);
    }

    /// <summary>
    /// Set the settings data on the current UI elements.
    /// </summary>
    public void SetData()
    {
        SettingsController.Instance.MusicSetting.Slider.value = MusicVolume;
        SettingsController.Instance.SFXSetting.Slider.value = SFXVolume;
        SettingsController.Instance.ResolutionSetting.CurrentIndex = ResolutionIndex;
        SettingsController.Instance.QualitySetting.CurrentIndex = QualityIndex;
        SettingsController.Instance.FullScreen.isOn = FullScreen;
        SettingsController.Instance.Vsync.isOn = Vsync;
        SettingsController.Instance.SameRule.isOn = SameRule;
        SettingsController.Instance.SameWallRule.isOn = SameWallRule;
        SettingsController.Instance.SuddenDeathRule.isOn = SuddenDeathRule;
        SettingsController.Instance.RandomRule.isOn = RandomRule;
        SettingsController.Instance.PlusRule.isOn = PlusRule;
        SettingsController.Instance.ElementalRule.isOn = ElementalRule;

    }
}
