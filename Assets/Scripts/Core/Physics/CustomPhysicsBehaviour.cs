using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomPhysicsBehaviour : MonoBehaviour
{
    [SerializeField] private Physics2DRaycaster raycaster;

    private void Awake()
    {
        GameController.Instance.OnRaycastUpdated += UpdateRaycast;
    }

    private void UpdateRaycast(LayerMask layer)
    {
        raycaster.eventMask = layer;
    }
}
