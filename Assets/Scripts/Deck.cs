using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    // this class will be responsible for managing the deck of cards in the game,
    // including shuffling, drawing, and (POSSIBLY)discarding cards.

    public List<Card_SO> cards, discardPile;
    public bool finished = false;

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
            Card_SO temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;

        }
        Debug.Log("Deck shuffled successfully");
    }

    public void LoadAllCards()
    {
        // this method will be responsible for loading all the cards from the resources folder into the deck,
        // folders for standard cards and wild cards, so that we can easily add new cards to the game by 
        // simply adding new assets to the resources folder
        Card_SO[] standardCards = Resources.LoadAll<Card_SO>("PlayingCards");
        Card_SO[] wildCards = Resources.LoadAll<Card_SO>("WildCards");

        foreach (Card_SO card in standardCards)
            cards.Add(Instantiate(card));

        foreach (Card_SO wildCard in wildCards)
            cards.Add(Instantiate(wildCard));
    }

    public void Awake()
    {
        cards = new List<Card_SO>();
        discardPile = new List<Card_SO>();

        LoadAllCards();

        // shuffles Fisher-Yates style 
        ShuffleDeck();

        // ensures the deck is fully initialised
        finished = true;
    }
}
