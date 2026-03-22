using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // this class will be responsible for managing the deck of cards in the game,
    // including shuffling, drawing, and (POSSIBLY)discarding cards.

    private List<Card> cards, discardPile;



    public void GenerateStandardDeck()
    {
        for (int suit = 0; suit < 4; suit++)
        {
            for (int rank = 2; rank <= 14; rank++)
            {
                Card card = new Card
                {
                    cardName = $"{(Suit)suit} {(Rank)rank}"
                };
                cards.Add(card);
            }
        }
    }
    public void RetrieveWildCards()
    {
        // this method will be responsible for retrieving all the wild cards from the resources folder,
        // and adding them to the deck's list of cards.
        WildCard[] wildCards = Resources.LoadAll<WildCard>("WildCards");

        foreach (WildCard wildCard in wildCards)
        {
            cards.Add(new WildCardInstance(wildCard));
        }
    }

    public void ShuffleDeck()
    {
        // this method will be responsible for shuffling the deck of cards,
        // want to use fisher-yates shuffle algorithm to ensure a good shuffle,
        // it's a cool one i found on stack overflow
        if (cards.Count == 0)
        {
            Debug.LogWarning("Deck is empty, cannot shuffle.");
            return;
        }
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;

        }
        Debug.Log("Deck shuffled successfully");
    }

    public void Awake()
    {
        cards = new List<Card>();
        discardPile = new List<Card>();


        // generates the standard 52 card deck, with 4 suits and 13 ranks each
        GenerateStandardDeck();

        // Puts all cards from resource folder into the deck
        // ready to be shuffled
        RetrieveWildCards();

        //shuffles Fisher-Yates style 
        ShuffleDeck();
    }
}
