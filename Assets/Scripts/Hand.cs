using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    //class for everything involving the hand
    //used for drawing cards at the start of the game, placing cards. 
    public List<Card> hand_cards;
    public int starting_size = 5;
    public Deck deck;
    public bool turn = false;
    public Card selected_card;
    public Transform cardLayout; 


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
        Card drawnCard = deck.cards[0];
        deck.cards.RemoveAt(0);
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

        // Nudge the card upward visually to show it is selected
        card.transform.localPosition = new Vector3(
            card.transform.localPosition.x,
            card.transform.localPosition.y + 20f,
            card.transform.localPosition.z
        );
    }
    public void DeselectCard()
    {
        if (selected_card == null) return;

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
    //checks if you have won
    public void WinCheck()
    {
        if (hand_cards.Count == 0)
        {
            SceneManager.LoadScene("Sc_GameOver");
        }
    }

    public void Awake()
    {
        hand_cards = new List<Card>();  //initialises the hand

        
    
    }

}