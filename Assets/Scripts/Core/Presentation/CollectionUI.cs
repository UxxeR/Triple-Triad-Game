using TMPro;
using UnityEngine;

public class CollectionUI : MonoBehaviour
{
    [field: SerializeField] public CanvasGroup CardInformationCanvasGroup { get; set; }
    [field: SerializeField] public TMP_Text CardNameText { get; set; }
    [field: SerializeField] public TMP_Text CardDescriptionText { get; set; }

    public void Start()
    {
        CollectionController.Instance.OnCardInformationVisibilityUpdated += UpdateCardInformationVisibility;
        CollectionController.Instance.OnCardInformationUpdated += UpdateCardInformation;
    }

    /// <summary>
    /// Update the visibility of the card information.
    /// </summary>
    /// <param name="alpha"></param>
    public void UpdateCardInformationVisibility(float alpha)
    {
        CardInformationCanvasGroup.alpha = alpha;
    }

    /// <summary>
    /// Update the card name and description.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public void UpdateCardInformation(string name, string description)
    {
        CardNameText.text = name;
        CardDescriptionText.text = description;
    }
}
