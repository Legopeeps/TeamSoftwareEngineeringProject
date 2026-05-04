using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState { StartGame, BeginPlayerTurn, EndPlayerTurn, EndGame }

public class GameManager : MonoBehaviour
{
    private GameState currentState;
    public Deck deck;
    public List<Hand> playerHands;
    public int initialHandSize = 1;
    public int currentPlayerIndex = 0;
    public Card_SO topCard;
    public Transform playPilePosition;
    public GameObject playerSwitchPanel, gameOverPanel;
    public TMP_Text playerSwitchText, gameOverText;


    void Start()
    {
        currentState = GameState.StartGame;
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
        topCard = card.card_SO;
        card.gameObject.SetActive(true);
        card.transform.SetParent(playPilePosition);  //moves the card to the play position
        card.transform.localPosition = Vector3.zero; //centres the card
        card.transform.localScale = Vector3.one; //resets the scale of the card to fit
    }

    void SetupGame()
    {
        Debug.Log("Setting up the game...");
        deck.InitialiseDeck();
        foreach (Hand hand in playerHands)
        {
            hand.SetupHand();
        }
        // assign each hand to the respective player variable
        for (int i = 0; i < initialHandSize; i++)
        {
            foreach (Hand hand in playerHands)
                hand.DrawCard();
        }

        FlipStartingCard();
        EnableHand(playerHands[currentPlayerIndex]);
        StartCoroutine(PlayerTurn(playerHands[currentPlayerIndex]));
    }

    IEnumerator PlayerTurn(Hand hand)
    {
        currentState = GameState.BeginPlayerTurn;
        //make it known that it's "this" players turn
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => currentState == GameState.EndPlayerTurn); //waits until the player has ended their turn
        WinCheck(hand);
        if (currentState == GameState.EndGame)
        {
            StartCoroutine(EndGame(hand));
        }
        else
        {
            DisableHand(hand);
            StartCoroutine(NextTurn());
        }
    }

    IEnumerator NextTurn()
    {
        //buffer to show what's happened in the game view
        yield return new WaitForSeconds(0.5f);
        playerSwitchText.text = $"{playerHands[(currentPlayerIndex + 1) % playerHands.Count].gameObject.name}'s Turn";
        playerSwitchPanel.SetActive(true); //next player panel revealed
        DisableHand(playerHands[currentPlayerIndex]); //previous players hand disabled
        currentPlayerIndex = (currentPlayerIndex + 1) % playerHands.Count; //update the current player to the next
        yield return new WaitForSeconds(2f);
        EnableHand(playerHands[currentPlayerIndex]);
        playerSwitchPanel.SetActive(false);
        yield return StartCoroutine(PlayerTurn(playerHands[currentPlayerIndex])); //on reveal, new player's turn is ready with their hand enabled
    }

    public void DrawCard()
    {
        playerHands[currentPlayerIndex].DrawCard();

        currentState = GameState.EndPlayerTurn;
    }


    bool IsPlayable(Card card)
    {
        return card.card_SO.suit == topCard.suit || card.card_SO.rank == topCard.rank || card.card_SO.isWildCard;  //returns true if the card is playable
    }

    public void TryPlayCard(Card card, Hand hand)
    {
        Debug.Log("TryPlayCard CHECK");
        if (IsPlayable(card))
        {
            Debug.Log($"Played {card.card_SO.rank} of {card.card_SO.suit}.");
            hand.hand_cards.Remove(card);   //removes a card from the hand and places it on the pile
            PlaceOnPile(card);
            currentState = GameState.EndPlayerTurn;
        }
        else
        {
            Debug.Log($"Can't play {card.card_SO.rank} of {card.card_SO.suit} � must match rank or suit.");
        }
    }

    IEnumerator EndGame(Hand hand)
    {
        gameOverPanel.SetActive(true);
        
        gameOverText.text = "Player " + hand.gameObject.name + " has Won!";

        yield return null;
    }

    public void WinCheck(Hand hand)
    {
        if (hand.hand_cards.Count == 0)
        {
            currentState = GameState.EndGame;
        }
    }

    /*
     * Hand Enabling and Disabling
     */

    void EnableHand(Hand hand)
    {
        hand.gameObject.SetActive(true);
    }
    void DisableHand(Hand hand)
    {
        hand.gameObject.SetActive(false);
    }


    /*
     * Coroutine management
     */
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
//        Debug.Log($"Can't play {card.card_SO.rank} of {card.card_SO.suit} � must match rank or suit.");
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
//        Debug.LogWarning("Deck is empty � no card to draw.");
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
//    StartTurn(  //goes to the next player
//}