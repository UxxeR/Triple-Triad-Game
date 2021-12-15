using UnityEngine;
using UnityEngine.EventSystems;

public class CustomPhysicsBehaviour : MonoBehaviour
{
    [SerializeField] private Physics2DRaycaster raycaster;

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        GameController.Instance.OnRaycastUpdated += UpdateRaycast;
    }

    /// <summary>
    /// Update the raycast physics.
    /// </summary>
    /// <param name="layer">New layer that will be interactable.</param>
    private void UpdateRaycast(LayerMask layer)
    {
        raycaster.eventMask = layer;
    }
}
