using UnityEngine;

///for deck generation
public enum Suit { Hearts, Diamonds, Clubs, Spades }
//named ranks, word values assigned explicit declaration within enum
public enum Rank { Ace = 1, Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack = 11, Queen = 12, King = 13 }

public class Card
{
    public string cardName;
    public virtual void Play()
    {

    }
}
