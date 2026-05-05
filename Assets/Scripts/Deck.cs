using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// this class will be responsible for managing the deck of cards in the game,
// including shuffling, drawing, and (POSSIBLY) discarding cards.
public class Deck : MonoBehaviour
{
    #region Variables
    public bool isDeckLoaded = false;
    public GameObject cardPrefab;
    public Transform deckHolderTransform;
    public List<Card> cards;
    #endregion

    private void ShuffleDeck()
    {
        // this method will be responsible for shuffling the deck of cards,
        // want to use fisher-yates shuffle algorithm to ensure a good shuffle
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

    private void LoadAllCards()
    {
        //From resources folder, takes playing cards & wildcards
        Card_SO[] standardCards = Resources.LoadAll<Card_SO>("PlayingCards");
        Card_SO[] wildCards = Resources.LoadAll<Card_SO>("WildCards");

        foreach (Card_SO cardSO in standardCards)
        {
            GameObject cardObject = Instantiate(cardPrefab, deckHolderTransform);
            cardObject.SetActive(false);
            Card card = cardObject.GetComponent<Card>();
            card.DisplayCard(cardSO);
            cards.Add(card); //loaded into list
        }
        Debug.Log($"Loaded {standardCards.Length} standard cards into the deck.");

        //foreach (Card_SO wildCard in wildCards)
        //    cards.Add(Instantiate(wildCard));
    }

    public void InitialiseDeck(List<Card> excludedCards = null)
    {
        cards = new List<Card>();
        LoadAllCards();

        if (excludedCards != null)
        {
            List<Card> heldCards = new List<Card>();

            if (excludedCards != null && excludedCards.Count > 0)
            {
                // Compare and remove those in game from new deck
                cards = cards.Except(excludedCards).ToList();
            }
            // Remove the held cards from the main deck
            cards = cards.Except(heldCards).ToList();
        }

        // shuffles Fisher-Yates style 
        ShuffleDeck();

        // ensures the deck is fully initialised
        isDeckLoaded = true;
    }
}
