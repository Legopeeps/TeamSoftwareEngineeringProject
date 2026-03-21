using System.Net.Security;
using UnityEngine;

public class Card : MonoBehaviour
{
    //playing cards (non wild)
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    void Start()
    {
        //standard 52 card deck generation
        for (int suit = 0; suit < 4; suit++)
        {
            for (int rank = 2; rank <= 14; rank++)
            {
                Debug.Log($"Card: {(Suit)suit} {((Rank)rank).ToString()}");
            }
        }

    }
}
