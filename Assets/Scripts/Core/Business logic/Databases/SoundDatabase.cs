using UnityEngine;

public class SoundDatabase : GenericDatabase<Sound>
{
   [SerializeField] private SoundTable soundTable;

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        Table = soundTable;
    }
}
