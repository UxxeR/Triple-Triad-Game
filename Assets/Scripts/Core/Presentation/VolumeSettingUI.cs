using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingUI : SliderSettingUI, ISetting
{
    [field: SerializeField] public string AudioMixerName { private get; set; }

    /// <summary>
    /// First method that will be called when a script is enabled. Only called once.
    /// </summary>
    private void Awake()
    {
        if (Value != null)
        {
            Value.gameObject.SetActive(!HideValue);
            Value.text = Mathf.FloorToInt(Slider.value * 100f).ToString();
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any of the Update methods are called the first time. Only called once.
    /// </summary>
    private void Start()
    {
        UpdateValue();
    }

    /// <summary>
    /// Update the slider and audio mixer values.
    /// </summary>
    public override void UpdateValue()
    {
        AudioController.Instance.AudioMixerGroup.audioMixer.SetFloat(AudioMixerName, Mathf.Log10(Slider.value) * 20);
        Value.text = Mathf.FloorToInt(Slider.value * 100f).ToString();
    }
}
