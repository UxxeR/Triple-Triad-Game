using System;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public static CollectionController Instance { get; private set; }
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform collectionWindow;
    public Action<float> OnCardInformationVisibilityUpdated;
    public Action<string, string> OnCardInformationUpdated;


    private void Awake()
    {
        Instance = this;
        PopulateCardCollection();
    }

    /// <summary>
    /// Update the visibility of the card information.
    /// </summary>
    /// <param name="alpha"></param>
    public void UpdateCardInformationVisibility(float alpha)
    {
        if (OnCardInformationVisibilityUpdated != null)
        {
            OnCardInformationVisibilityUpdated(alpha);
        }
    }

    /// <summary>
    /// Update the card name and description.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    public void UpdateCardInformation(string name, string description)
    {
        if (OnCardInformationUpdated != null)
        {
            OnCardInformationUpdated(name, description);
        }
    }

    /// <summary>
    /// Populete the collection with all the cards in the game. Those that are not owned by the player will not be interactables.
    /// </summary>
    public void PopulateCardCollection()
    {
        CardDatabase.Instance.GetAllElement().ForEach(cardData =>
        {
            CollectionCard card = Instantiate(cardPrefab, collectionWindow, false).GetComponent<CollectionCard>();
            card.CardData = cardData;

            if (!Player.Instance.UnlockedIdCards.Contains(card.CardData.Id))
            {
                card.CanvasGroup.interactable = false;
                card.CanvasGroup.alpha = 0.7f;
            }
        });
    }
}
