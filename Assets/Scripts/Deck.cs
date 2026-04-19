using System.Collections.Generic;
using UnityEngine;


// this class will be responsible for managing the deck of cards in the game,
// including shuffling, drawing, and (POSSIBLY)discarding cards.
public class Deck : MonoBehaviour
{

    public List<Card_SO> cardsSO;
    public bool finished = false;
    public GameObject cardPrefab;
    public List<Card> cards;

    public void ShuffleDeck()
    {
        // this method will be responsible for shuffling the deck of cards,
        // want to use fisher-yates shuffle algorithm to ensure a good shuffle,
        // it's a cool one i found on stack overflow
        if (cardsSO.Count == 0)
        {
            Debug.LogWarning("Deck is empty, cannot shuffle.");
            return;
        }
        for (int i = cardsSO.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card_SO temp = cardsSO[i];
            cardsSO[i] = cardsSO[j];
            cardsSO[j] = temp;
        }
        Debug.Log("Deck shuffled successfully");
    }

    public void LoadAllCards()
    {
        //From resources folder, takes playing cards & wildcards
        Card_SO[] standardCards = Resources.LoadAll<Card_SO>("PlayingCards");
        Card_SO[] wildCards = Resources.LoadAll<Card_SO>("WildCards");

        foreach (Card_SO cardSO in standardCards)
        {
            GameObject cardObject = Instantiate(cardPrefab);
            Card card = cardObject.GetComponent<Card>();
            card.DisplayCard(cardSO);
            cards.Add(card);
        }
        Debug.Log($"Loaded {standardCards.Length} standard cards into the deck.");

        //foreach (Card_SO wildCard in wildCards)
        //    cards.Add(Instantiate(wildCard));
    }

    public void Awake()
    {
        cardsSO = new List<Card_SO>();
        cards = new List<Card>();
        LoadAllCards();

        // shuffles Fisher-Yates style 
        ShuffleDeck();

        // ensures the deck is fully initialised
        finished = true;
    }
}
