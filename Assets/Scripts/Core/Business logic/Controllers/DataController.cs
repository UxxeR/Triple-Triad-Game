using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance { get; private set; }
    public SettingsData Settings { get; set; }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        LoadSettings();
    }

    /// <summary>
    /// Load the saved settings or create a default ones.
    /// </summary>
    private void LoadSettings()
    {
        try
        {
            Settings = (SettingsData)DataSerialization.Load("Settings");
        }
        catch (Exception exception)
        {
            Debug.LogWarning(exception);
            Settings = new SettingsData();
        }
    }
}
