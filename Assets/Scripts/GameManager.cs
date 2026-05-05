using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Linq;

public enum GameState { StartGame, BeginPlayerTurn, EndPlayerTurn, EndGame }

public class GameManager : MonoBehaviour
{
    #region Variables
    private GameState currentState;
    private bool canDraw = true;

    public Deck deck;
    public List<Hand> playerHands;
    public int initialHandSize = 1;
    public int currentPlayerIndex = 0;
    public Card_SO topCard;
    public Transform playPilePosition;
    public TMP_Text invalidMoveText;
    public GameObject playerSwitchPanel, gameOverPanel;
    public TMP_Text playerSwitchText, gameOverText;
    public GameObject scrollView;

    #endregion
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
        currentState = GameState.BeginPlayerTurn; //make it known that it's "this" players turn
        //scrollview x position is set to 0 at the start of the turn
        scrollView.transform.localPosition = new Vector3(0, scrollView.transform.localPosition.y, scrollView.transform.localPosition.z);
        canDraw = true; //allows the player to draw a card at the start of their turn, but not after they have drawn


        yield return new WaitUntil(() => currentState == GameState.EndPlayerTurn); //waits until the player has ended their turn

        WinCheck(hand);
        if (currentState == GameState.EndGame)
        {
            StartCoroutine(EndGame(hand));
        }
        else
        {
            //DisableHand(hand);
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
        if (canDraw)
        {
            if (deck.cards.Count > 0)
            {
                playerHands[currentPlayerIndex].DrawCard();
                canDraw = false;
                currentState = GameState.EndPlayerTurn;
            }
            else
            {
                //collects data of all the cards currently held by players to reshuffle into the deck
                List<Card> allHeldCards = playerHands.SelectMany(h => h.heldCards).ToList();

                Card currentTopCard = playPilePosition.GetComponentInChildren<Card>();
                if (currentTopCard != null)
                {
                    allHeldCards.Add(currentTopCard);
                }

                //reloads all the cards into the deck and reshuffles
                deck.InitialiseDeck(allHeldCards);

                //player draws a card after reshuffling
                DrawCard();
            }
        }
    }


    bool IsPlayable(Card card)
    {
        return card.card_SO.suit == topCard.suit || card.card_SO.rank == topCard.rank || card.card_SO.isWildCard;  //returns true if the card is playable
    }

    public async Task TryPlayCard(Card card, Hand hand)
    {
        Debug.Log("TryPlayCard CHECK");
        if (IsPlayable(card))
        {
            Debug.Log($"Played {card.card_SO.rank} of {card.card_SO.suit}.");
            hand.heldCards.Remove(card);   //removes a card from the hand and places it on the pile
            PlaceOnPile(card);
            currentState = GameState.EndPlayerTurn;
        }
        else
        {
            DisableHand(playerHands[currentPlayerIndex]);
            invalidMoveText.gameObject.SetActive(true);
            await Task.Delay(2000); //waits for 2 seconds to show the invalid move text
            invalidMoveText.gameObject.SetActive(false);
            EnableHand(playerHands[currentPlayerIndex]);
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
        if (hand.heldCards.Count == 0)
        {
            currentState = GameState.EndGame;
        }
    }

    #region Hand Enabling and Disabling
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
    #endregion

    #region Coroutine Management
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
    #endregion

}
