using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundTable", menuName = "Database/SoundTable", order = 0)]
public class SoundTable : ScriptableObject, ITable<Sound>
{
    public string Id => "SoundTable";
    [field: SerializeField] public List<Sound> Elements { get; set; }
}
