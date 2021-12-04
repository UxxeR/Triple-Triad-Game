using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour
{
    public SpriteRenderer background;
    public TMP_Text powerText;
    [SerializeField] private Vector3 raycastVector;
    public SideName sideName;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, raycastVector);
    }
}

public enum SideName
{
    UP = 0,
    RIGHT = 1,
    DOWN = 2,
    LEFT = 3
}