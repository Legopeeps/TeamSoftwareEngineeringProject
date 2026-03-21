using UnityEngine;

[CreateAssetMenu(fileName = "New Wild Card", menuName = "Wild Card")]
public class WildCard : ScriptableObject
{

    // on generation of asset, each can be modified in the inspector to have different values,
    // so that we can have a variety of wild cards in the game
    public string cardName;
    public int value;
    public string description;

    // all wild cards created have "isWildCard" set to true,
    // so that they can be easily identified as wild cards in the game logic,
    // seperate from regular playing cards, as we will be making lots of wilds
    private bool isWildCard = true;
}