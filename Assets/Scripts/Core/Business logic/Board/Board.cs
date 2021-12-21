using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [field: SerializeField] public List<Slot> Slots { get; set; } = new List<Slot>();
    [field: SerializeField] public List<Card> Walls { get; set; } = new List<Card>();

    private void Awake()
    {
        if (!DataController.Instance.Settings.SameWallRule)
        {
            Walls.ForEach(wall => wall.gameObject.SetActive(false));
        }
    }
}
