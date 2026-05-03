using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Hand : MonoBehaviour
{
    //class for everything involving the hand
    //used for drawing cards at the start of the game, placing cards. 
    public List<Card> hand_cards;

    public int starting_size = 5;
    public Deck deck;
    public Card selected_card;
    public Transform cardLayout; 


    public void SetupHand()
    {
        hand_cards = new List<Card>();
    }

    //gets the starting hand for the player

    public void GetStartingHand()
    {
        for (int i = 0; i < starting_size; i++)
        {
            DrawCard();
        }
    }

    //draws a card from the deck
    public void DrawCard()
    {
        Card drawnCard = deck.cards.First();
        deck.cards.Remove(drawnCard);
        drawnCard.gameObject.SetActive(true);
        drawnCard.transform.SetParent(cardLayout, false);
        drawnCard.transform.localScale = Vector3.one;
        hand_cards.Add(drawnCard);
    }

    public void SelectCard(Card card)
    {

        // Deselect the previous card if there was one
        if (selected_card != null)
            DeselectCard();

        selected_card = card;
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
        if (selected_card == null) return;
        selected_card.selected = false;
        selected_card.transform.localPosition = new Vector3(
            selected_card.transform.localPosition.x,
            selected_card.transform.localPosition.y - 20f,
            selected_card.transform.localPosition.z
        );

        selected_card = null;
    }

    public bool HasPlayableCard(Card_SO topCard)
    {
        foreach (Card card in hand_cards)
        {
            if (card.card_SO.suit == topCard.suit || card.card_SO.rank == topCard.rank)
                return true;
        }
        return false;

    }
}