using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckData
{
    public string Id { get; set; }
    public List<string> CardIds { get; set; } = new List<string>();
    public bool Active { get; set; }

    public DeckData()
    {
    }

    public DeckData(string Id, List<string> CardIds, bool Active = false)
    {
        this.Id = Id;
        this.CardIds = CardIds;
        this.Active = Active;
    }
}
