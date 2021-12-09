using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [field: SerializeField] public Slot[] Slots { get; set; } = new Slot[9];
}
