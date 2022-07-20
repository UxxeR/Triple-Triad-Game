using System;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance { get; private set; }
    public PlayerData PlayerData { get; set; }
    public SettingsData SettingData { get; set; }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        LoadSettings();
        LoadPlayer();
    }

    /// <summary>
    /// Load the saved settings or create a default ones.
    /// </summary>
    private void LoadSettings()
    {
        try
        {
            SettingData = (SettingsData)DataSerialization.Load("Settings");
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
            SettingData = new SettingsData();
        }
    }

    private void LoadPlayer()
    {
        try
        {
            PlayerData = (PlayerData)DataSerialization.Load("Player");
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
            PlayerData = new PlayerData();
        }

        PlayerData.SetData();
    }

    public void SavePlayerData()
    {
        DataSerialization.Save("Player", PlayerData.GetData());
    }
}
