using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card_SO : ScriptableObject
{

    // on generation of asset, each can be modified in the inspector to have different values,
    // so that we can have a variety of cards and wild cards in the game
    public string cardName;
    public int value;
    public string description;

    // all wild cards created have "isWildCard" set to true,
    // so that they can be easily identified as wild cards in the game logic,
    // seperate from regular playing cards, as we will be making lots of wilds
    private bool isWildCard = false;

    public void PlayEffect()
    {
        Debug.Log("Playing effect of " + cardName);
    }
}