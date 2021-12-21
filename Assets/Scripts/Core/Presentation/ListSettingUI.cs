using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ListSettingUI<T> : ISetting
{
    [field: SerializeField] public Button AddButton { get; set; }
    [field: SerializeField] public Button SubtractButton { get; set; }
    [field: SerializeField] public TMP_Text Value { get; private set; }
    [field: SerializeField] public List<T> Values { get; set; }
    public int CurrentIndex { get; set; }

    /// <summary>
    /// Update the list value and enable or disable the update buttons if the current value is on his upper or bottom limit.
    /// </summary>
    public void UpdateValue()
    {
        if (CurrentIndex == 0)
        {
            AddButton.interactable = true;
            SubtractButton.interactable = false;
        }
        else if (CurrentIndex == Values.Count - 1)
        {
            AddButton.interactable = false;
            SubtractButton.interactable = true;
        }
        else
        {
            SubtractButton.interactable = true;
            AddButton.interactable = true;
        }

        Value.text = Values[CurrentIndex].ToString().Split('@')[0];
    }

    /// <summary>
    /// Modify the current list index and update the text with his value.
    /// </summary>
    /// <param name="indexModification">Index modification.</param>
    public void ModifyIndex(int indexModification)
    {
        CurrentIndex += indexModification;
        UpdateValue();
    }
}
