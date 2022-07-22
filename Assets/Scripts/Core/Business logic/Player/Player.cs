using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; set; }
    public List<string> UnlockedIdCards { get; set; } = new List<string>();
    public List<DeckData> Decks { get; set; } = new List<DeckData>();
    public DeckData CurrentDeck { get; set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
