using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomPhysicsBehaviour : MonoBehaviour
{
    public static CustomPhysicsBehaviour instance;
    public Physics2DRaycaster raycaster;

    private void Awake()
    {
        instance = this;
    }
}
