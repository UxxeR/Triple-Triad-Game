using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderSettingUI : MonoBehaviour
{
    [field: SerializeField] public Slider Slider { get; set; }
    [field: SerializeField] public TMP_Text Value { get; set; }
    [field: SerializeField] public bool HideValue { get; set; }

    /// <summary>
    /// Update the slider and audio mixer values.
    /// </summary>
    public abstract void UpdateValue();
}
