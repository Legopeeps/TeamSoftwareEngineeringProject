using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public enum GameState { Start, PlayerTurn, End}

public class GameManager : MonoBehaviour
{
    private GameState currentState;
    public Deck deck;
    public List<Hand> playerHands;
    public int initialHandSize = 5;
    public Card topCard;
    public Transform playPilePosition;
    public TMP_Text turnAnnouncerText; //ui elements
    public TMP_Text topCardText;

    private void OnEnable()
    {
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    void Start()
    {
        currentState = GameState.Start;
        SetupGame();
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
        topCard = card;    
        card.gameObject.SetActive(true);      
        card.transform.SetParent(playPilePosition);  //moves the card to the play position
        card.transform.localPosition = Vector3.zero; //centres the card
        //card.transform.localscale = Vector3.one;

        if (topCardText != null)
            topCardText.text = $"Top: {card.card_SO.rank} of {card.card_SO.suit}"; //updates ui for the top card
    }

    void SetupGame()
    {
        Debug.Log("Setting up the game...");
        deck.InitialiseDeck();
        // assign each hand to the respective player variable
        for (int i = 0; i < initialHandSize; i++)
        {
            foreach (Hand hand in playerHands)
                hand.DrawCard();
        }

        FlipStartingCard();

        GameLoop();

        // turn
    }

    void GameLoop()
    {
        //while (currentState != GameState.End)
        //{
        //   //foreach(Hand hand in playerHands)
        //   //{
        //   //    //make it known that it's "this" players turn
        //   //    yield return new WaitForSeconds(2f);
        //   //    currentState = GameState.PlayerTurn;
        //   //    EnableHand(hand);
        //   //    //turn goes here
        //   //
        //   //    if(hand.hand_cards.Count == 0)
        //   //    {
        //   //        currentState = GameState.End;
        //   //        Debug.Log($"Player [player name here] has won!");
        //   //        break;
        //   //    }
        //   //    DisableHand(hand);
        //   //}
        //}
        //yield return null;
    }

    void EnableHand(Hand hand)
    {
        hand.gameObject.SetActive(true);
    }
    void DisableHand(Hand hand) 
    {
        hand.gameObject.SetActive(false);
    }

    IEnumerator PlayerTurn(Hand hand) { 
        // logic for player's turn
        
        yield return null; // placeholder for turn duration
    }

    bool IsPlayable(Card card)
    {
       return card.card_SO.suit == topCard.card_SO.suit || card.card_SO.rank == topCard.card_SO.rank;  //returns true if the card is playable
    }

    public void TryPlayCard(Card card, Hand hand)
    {
        Debug.Log($"TryPlayCard has been called");
        if (!hand.turn) return;    //ignores the other players
    
        if (IsPlayable(card))
        {
            Debug.Log($"Played {card.card_SO.rank} of {card.card_SO.suit}.");
            hand.hand_cards.Remove(card);   //removes a card from the hand and places it on the pile
            PlaceOnPile(card);
            hand.WinCheck();  //trys wincheck
            //EndTurn();
        }
        else
        {
            Debug.Log($"Can't play {card.card_SO.rank} of {card.card_SO.suit} — must match rank or suit.");
        }
    }

}























//public Deck deck;
//public List<Hand> players;   //list for the hand objects
//  

//public Button drawPileButton;
//private int currentPlayerIndex = 0; //player turn
//private Card_SO topCard; //stores the data of the last card played
//private readonly string[] playerNames = { "Player 1", "Player 2", "Player 3", "Player 4" }; //player names


//void Start()
//{
//    foreach (Hand hand in players) //draws the starting hand for each player
//        hand.GetStartingHand();
//
//    FlipStartingCard();  //places the first card on the pile
//    StartTurn(0);  //starts the game with player 1 
//}
//
//





//void StartTurn(int playerIndex)
//{
//    currentPlayerIndex = playerIndex;
//
//    for (int i = 0; i < players.Count; i++)   //makes it so the current playres turn is true but false for all others
//        players[i].turn = (i == currentPlayerIndex);
//
//    if (turnAnnouncerText != null)  //changes ui to show the current turn
//        turnAnnouncerText.text = $"{playerNames[currentPlayerIndex]}'s Turn";
//
//    Debug.Log($"--- {playerNames[currentPlayerIndex]}'s Turn ---");
//}


//public void TryPlayCard(Card card, Hand hand)
//{
//    if (!hand.turn) return;    //ignores the other players
//
//    if (IsPlayable(card.card_SO))
//    {
//        hand.hand_cards.Remove(card);   //removes a card from the hand and places it on the pile
//        PlaceOnPile(card);
//        hand.WinCheck();  //trys wincheck
//        EndTurn();
//    }
//    else
//    {
//        Debug.Log($"Can't play {card.card_SO.rank} of {card.card_SO.suit} — must match rank or suit.");
//    }
//}

//public void DrawAndPass()
//{
//    Hand current = players[currentPlayerIndex];  
//    if (!current.turn) return;
//
//    if (deck.cards.Count > 0) //draws the top card into the players hand
//    {
//        current.DrawCard();
//        Debug.Log($"{playerNames[currentPlayerIndex]} drew a card.");
//    }
//    else
//    {
//        Debug.LogWarning("Deck is empty — no card to draw.");
//    }
//
//    EndTurn();
//}
//
//bool IsPlayable(Card_SO card)
//{
//    return card.suit == topCard.suit || card.rank == topCard.rank;  //returns true if the card is playable
//}
//
//void EndTurn()
//{
//    StartTurn((currentPlayerIndex + 1) % players.Count);  //goes to the next player
//}