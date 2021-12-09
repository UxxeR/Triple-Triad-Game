using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] private Vector3 raycastOffset;
    [SerializeField] private Vector3 raycastVector;
    [SerializeField] private SideName sideName;
    [field: SerializeField] public SpriteRenderer Background { get; set; }
    [field: SerializeField] public TMP_Text PowerText { get; set; }

    public Card GetTarget()
    {
        LayerMask hitLaterMask = 1 << LayerMask.NameToLayer("PlayerCard") | 1 << LayerMask.NameToLayer("EnemyCard");
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position + this.raycastOffset, this.raycastVector, 1f, hitLaterMask);
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