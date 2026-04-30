using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Deck deck;
    public List<Hand> players;   //list for the hand objects
    public Transform playPilePosition;  
    public TextMeshProUGUI turnAnnouncerText; //ui elements
    public TextMeshProUGUI topCardText;
    public Button drawPileButton;
    private int currentPlayerIndex = 0; //player turn
    private Card_SO topCard; //stores the data of the last card played
    private readonly string[] playerNames = { "Player 1", "Player 2", "Player 3", "Player 4" }; //player names


    void Start()
    {
        foreach (Hand hand in players) //draws the starting hand for each player
            hand.GetStartingHand();

        FlipStartingCard();  //places the first card on the pile
        StartTurn(0);  //starts the game with player 1 
    }


    void FlipStartingCard()
    {
        if (deck.cards.Count == 0) return;  

        Card firstCard = deck.cards[0];  //sets the first card in a variable
        deck.cards.RemoveAt(0);
        PlaceOnPile(firstCard);  //places it on the pile
    }

    void PlaceOnPile(Card card)
    {
        topCard = card.card_SO;    
        card.gameObject.SetActive(true);      
        card.transform.SetParent(playPilePosition);  //moves the card to the play position
        card.transform.localPosition = Vector3.zero; //centres the card
        //card.transform.localscale = Vector3.one;

        if (topCardText != null)
            topCardText.text = $"Top: {card.card_SO.rank} of {card.card_SO.suit}"; //updates ui for the top card
    }


    void StartTurn(int playerIndex)
    {
        currentPlayerIndex = playerIndex;

        for (int i = 0; i < players.Count; i++)   //makes it so the current playres turn is true but false for all others
            players[i].turn = (i == currentPlayerIndex);

        if (turnAnnouncerText != null)  //changes ui to show the current turn
            turnAnnouncerText.text = $"{playerNames[currentPlayerIndex]}'s Turn";

        Debug.Log($"--- {playerNames[currentPlayerIndex]}'s Turn ---");
    }


    public void TryPlayCard(Card card, Hand hand)
    {
        if (!hand.turn) return;    //ignores the other players

        if (IsPlayable(card.card_SO))
        {
            hand.hand_cards.Remove(card);   //removes a card from the hand and places it on the pile
            PlaceOnPile(card);
            hand.WinCheck();  //trys wincheck
            EndTurn();
        }
        else
        {
            Debug.Log($"Can't play {card.card_SO.rank} of {card.card_SO.suit} — must match rank or suit.");
        }
    }

    public void DrawAndPass()
    {
        Hand current = players[currentPlayerIndex];  
        if (!current.turn) return;

        if (deck.cards.Count > 0) //draws the top card into the players hand
        {
            current.DrawCard();
            Debug.Log($"{playerNames[currentPlayerIndex]} drew a card.");
        }
        else
        {
            Debug.LogWarning("Deck is empty — no card to draw.");
        }

        EndTurn();
    }

    bool IsPlayable(Card_SO card)
    {
        return card.suit == topCard.suit || card.rank == topCard.rank;  //returns true if the card is playable
    }

    void EndTurn()
    {
        StartTurn((currentPlayerIndex + 1) % players.Count);  //goes to the next player
    }
}
