using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingUI : SliderSettingUI, ISetting
{
    [field: SerializeField] public string AudioMixerName { private get; set; }

    private void Awake()
    {
        if (Value != null)
        {
            Value.gameObject.SetActive(!HideValue);
            Value.text = Mathf.FloorToInt(Slider.value * 100f).ToString();
        }
    }

    private void Start()
    {
        UpdateValue();
    }

    public override void UpdateValue()
    {
        AudioController.Instance.AudioMixerGroup.audioMixer.SetFloat(AudioMixerName, Mathf.Log10(Slider.value) * 20);
        Value.text = Mathf.FloorToInt(Slider.value * 100f).ToString();
    }
}
