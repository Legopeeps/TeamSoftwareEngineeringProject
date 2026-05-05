using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;


//class for drawing cards, selecting cards,
//and the check if the card is playable
public class Hand : MonoBehaviour
{    
    public List<Card> heldCards;
    public Deck deck;
    public Card selectedCard;
    public Transform cardLayout;

    public void SetupHand()
    {
        heldCards = new List<Card>();
    }

    //draws a card from the deck
    public void DrawCard()
    {
        Card drawnCard = deck.cards.First();
        deck.cards.Remove(drawnCard);
        drawnCard.gameObject.SetActive(true);
        drawnCard.transform.SetParent(cardLayout, false);
        drawnCard.transform.localScale = Vector3.one;
        heldCards.Add(drawnCard);
    }

    public void SelectCard(Card card)
    {

        // Deselect the previous card if there was one
        if (selectedCard != null)
            DeselectCard();

        selectedCard = card;
        card.selected = true;
        // Nudge the card upward visually to show it is selected
        card.transform.localPosition = new Vector3(
            card.transform.localPosition.x,
            card.transform.localPosition.y + 20f,
            card.transform.localPosition.z
        );
        Debug.Log($"Selected card: {card.card_SO.rank} of {card.card_SO.suit}");
    }

    public void DeselectCard()
    {
        if (selectedCard == null) return;
        selectedCard.selected = false;
        selectedCard.transform.localPosition = new Vector3(
            selectedCard.transform.localPosition.x,
            selectedCard.transform.localPosition.y - 20f,
            selectedCard.transform.localPosition.z
        );

        selectedCard = null;
    }

    public bool HasPlayableCard(Card_SO topCard)
    {
        foreach (Card card in heldCards)
        {
            if (card.card_SO.suit == topCard.suit || card.card_SO.rank == topCard.rank)
                return true;
        }
        return false;

    }
}