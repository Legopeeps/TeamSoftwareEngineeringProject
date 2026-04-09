using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card_SO : ScriptableObject
{
    public string cardName;
    public enum suit { Hearts, Diamonds, Clubs, Spades };
    public Rank rank;
    public Sprite artwork;

    // all wild cards created have "isWildCard" set to true,
    // so that they can be easily identified as wild cards in the game logic,
    // seperate from regular playing cards, as we will be making lots of wilds
    public bool isWildCard;

    [TextArea]
    public string description;

    public void PlayEffect()
    {
        Debug.Log("Playing effect of " + cardName);
    }
}