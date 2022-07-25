using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class PlayerData : IData<PlayerData>
{
    public List<string> UnlockedIdCards { get; set; } = new List<string>();
    public List<DeckData> Decks { get; set; } = new List<DeckData>();
    public DeckData CurrentDeck { get; set; }

    public PlayerData()
    {
        UnlockedIdCards = new List<string>() { "card", "card1", "card2", "card3", "card4" };
        Decks = new List<DeckData>() { new DeckData("default", new List<string>() { "card", "card1", "card2", "card3", "card4" }, true) };
        CurrentDeck = Decks.FirstOrDefault();
    }

    public PlayerData(List<string> UnlockedCards, List<DeckData> Decks, DeckData CurrentDeck)
    {
        this.UnlockedIdCards = UnlockedCards;
        this.Decks = Decks;
        this.CurrentDeck = CurrentDeck;
    }

    /// <summary>
    /// Get the player data.
    /// </summary>
    /// <returns>The player data.</returns>
    public PlayerData GetData()
    {
        this.UnlockedIdCards = Player.Instance.UnlockedIdCards;
        this.Decks = Player.Instance.Decks;
        return new PlayerData(this.UnlockedIdCards, this.Decks, this.CurrentDeck);
    }

    /// <summary>
    /// Set the player data.
    /// </summary>
    public void SetData()
    {
        Player.Instance.UnlockedIdCards = this.UnlockedIdCards;
        Player.Instance.Decks = this.Decks;
        Player.Instance.CurrentDeck = this.CurrentDeck;
    }
}
