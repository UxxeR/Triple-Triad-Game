using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    public static DataController Instance;
    public SettingsData Settings { get; set; }

    private void Awake()
    {
        Instance = this;

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
