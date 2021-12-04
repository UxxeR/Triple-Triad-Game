using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData card;
    public SpriteRenderer background;
    public SpriteRenderer cardSprite;
    public SpriteRenderer elementSprite;
    public Side[] sides = new Side[4];
    public Team team;

    private void Awake()
    {
        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].powerText.text = this.card.power[i].ToString();
        }

        UpdateTeam(this.team);
    }

    public void UpdateTeam(Team team)
    {
        this.team = team;
        this.background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();
        
        for (int i = 0; i < sides.Length; i++)
        {
            this.sides[i].background.color = GenericAttribute.GetAttribute<CustomColorAttribute>(team).HexadecimalToRGBColor();
        }
    }
}

public enum Team
{
    [CustomColor("#222A5EFF")] BLUE = 0,
    [CustomColor("#5E2228FF")] RED = 1
}
