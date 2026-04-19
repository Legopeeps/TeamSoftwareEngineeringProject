using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    //class for everything involving the hand
    //used for drawing cards at the start of the game, placing cards. 
    public List<Card> hand_cards;
    public int starting_size = 5;
    public GameObject link;
    public Deck deck;
    public bool turn = false;
    public bool win = true;
    public Card selected_card;

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
        hand_cards.Add(deck.cards[0]);
        deck.cards.RemoveAt(0);    //removes the card fron the deck
    }

    //checks if you have won
    public void WinCheck()
    {
        if (hand_cards.Count == 0)
        {
            win = true;

            SceneManager.LoadScene("Sc_GameOver");
        }
    }

    //public void Awake()
    //{
    //    hand_cards = new List<Card>();  //initialises the hand
    //
    //    if (deck.finished == true)      //checks if the deck has finished being created and shuffled
    //    {
    //        deck = link.GetComponent<Deck>();
    //        GetStartingHand();
    //    }
    //
    //}

    //public void Update()
    //{
    //    if (deck.finished == true)      //checks if the deck has finished being created and shuffled
    //    {
    //        deck = link.GetComponent<Deck>();
    //        GetStartingHand();
    //        deck.finished = false;
    //    }
    //
    //    while (turn == true)
    //    {
    //        WinCheck();    //checks if won
    //    }
    //}


}