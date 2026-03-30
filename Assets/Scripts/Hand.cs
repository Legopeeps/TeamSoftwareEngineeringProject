using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{


    //class for everything involving the hand
    //used for drawing cards at the start of the game, placing cards. 
    public List<Card_SO> hand_cards;
    public int starting_size = 5;
    public GameObject link;
    public Deck deck;
    public bool turn = false;
    public bool win = true;
    public Card_SO selected_card;

    //gets the starting hand for the player

    public void get_starting_hand()
    {
        for (int i = 0; i < starting_size; i++)
        {
            draw_card();
        }
    }

    //draws a card from the deck
    public void draw_card()
    {
        hand_cards.Add(deck.cards[0]);
        deck.cards.RemoveAt(0);    //removes the card fron the deck
    }

    //checks if you have won
    public void win_check()
    {
        if (hand_cards.Count == 0)
        {
            win = true;

            SceneManager.LoadScene("Sc_GameOver");
        }
    }

    public void Awake()
    {
        hand_cards = new List<Card_SO>();  //initialises the hand

        if (deck.finished == true)      //checks if the deck has finished being created and shuffled
        {
            deck = link.GetComponent<Deck>();
            get_starting_hand();
        }

    }

    public void update()
    {
        if (deck.finished == true)      //checks if the deck has finished being created and shuffled
        {
            deck = link.GetComponent<Deck>();
            get_starting_hand();
            deck.finished = false;
        }

        while (turn == true)
        {
            win_check();    //checks if won
        }
    }


}