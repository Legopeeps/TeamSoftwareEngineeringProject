using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{
    
    
    //class for everything involving the hand
    //used for drawing cards at the start of the game, placing cards. 
    private List<Card> hand_cards;
    public int starting_size = 5;
    public GameObject link;
    public Deck deck;

    //gets the starting hand for the player

    public void get_starting_hand()
    {
        for (int i = 0; i < starting_size; i++)
        {
            hand_cards.Add(deck.cards[0]);
            deck.cards.RemoveAt(0);
        } 

    }

    public void Awake()
    {
        hand_cards = new List<Card>();
        deck = link.GetComponent<Deck>();
        get_starting_hand();
    }
}