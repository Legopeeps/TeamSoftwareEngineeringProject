using UnityEngine;

public class WildCardInstance : Card
{
    private WildCard data;

    public WildCardInstance(WildCard wildCard)
    {
        data = wildCard;
        cardName = wildCard.cardName;
    }

    public override void Play()
    {
        data.PlayEffect();
    }
}