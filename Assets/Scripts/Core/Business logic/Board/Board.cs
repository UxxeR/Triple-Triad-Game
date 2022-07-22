using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [field: SerializeField] public List<Slot> Slots { get; set; } = new List<Slot>();
    [field: SerializeField] public List<Card> Walls { get; set; } = new List<Card>();

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        if (!DataController.Instance.SettingData.SameWallRule)
        {
            Walls.ForEach(wall => wall.gameObject.SetActive(false));
        }
    }
}
