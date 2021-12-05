using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour
{
    public SpriteRenderer background;
    public TMP_Text powerText;
    [SerializeField] private Vector3 raycastOffset;
    [SerializeField] private Vector3 raycastVector;
    public SideName sideName;

    public Card GetTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + this.raycastOffset, this.raycastVector, 1f, 1 << LayerMask.NameToLayer("Card"));
        return hit.transform?.gameObject.GetComponent<Card>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position + this.raycastOffset, this.raycastVector);
    }
}

public enum SideName
{
    UP = 0,
    RIGHT = 1,
    DOWN = 2,
    LEFT = 3
}