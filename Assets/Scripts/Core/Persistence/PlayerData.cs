using System.Collections.Generic;

[System.Serializable]
public class PlayerData : IData<PlayerData>
{
    public List<string> UnlockedIdCards { get; set; } = new List<string>();
    public List<string> IdDecks { get; set; } = new List<string>();

    public PlayerData()
    {
        UnlockedIdCards = new List<string>() { "card", "card1", "card2", "card3", "card4" };
        IdDecks = new List<string>() { "default" };
    }

    public PlayerData(List<string> unlockedCards, List<string> decks)
    {
        UnlockedIdCards = unlockedCards;
        IdDecks = decks;
    }

    public PlayerData GetData()
    {
        this.UnlockedIdCards = Player.Instance.UnlockedIdCards;
        this.IdDecks = Player.Instance.IdDecks;
        return new PlayerData(this.UnlockedIdCards, this.IdDecks);
    }

    public void SetData()
    {
        Player.Instance.UnlockedIdCards = this.UnlockedIdCards;
        Player.Instance.IdDecks = this.IdDecks;
    }
}
