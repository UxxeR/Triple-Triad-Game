using TMPro;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public static CollectionController Instance { get; private set; }
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform collectionWindow;
    [field: SerializeField] public CanvasGroup CardInformationCanvasGroup { get; set; }
    [field: SerializeField] public TMP_Text CardNameText { get; set; }
    [field: SerializeField] public TMP_Text CardDescriptionText { get; set; }

    private void Awake()
    {
        Instance = this;
        CardDatabase.Instance.GetAllElement().ForEach(cardData =>
        {
            CollectionCard card = Instantiate(cardPrefab, collectionWindow, false).GetComponent<CollectionCard>();
            card.CardData = cardData;

            if (!Player.Instance.UnlockedIdCards.Contains(card.CardData.Id))
            {
                card.CanvasGroup.interactable = false;
            }
        });
    }
}
