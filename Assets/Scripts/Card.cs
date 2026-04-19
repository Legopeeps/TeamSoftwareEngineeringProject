using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Named ranks, word values assigned explicit declaration within enum
public enum Rank { Ace = 1, Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack = 11, Queen = 12, King = 13 }
public enum Suit { Clubs, Hearts, Spades, Diamonds };

public class Card : MonoBehaviour
{
    public Card_SO card_SO; // Empty in inspector, code grabs the respective SO object
    public Image suitImage; // Final location of Sprite on card
    public Sprite clubsSPR, heartsSPR, spadesSPR, diamondsSPR; // Potential sprites
    public TextMeshProUGUI cardRankText, cardScoreText; // Text locations

    public void DisplayCard(Card_SO card_SOToDisplay)
    {
        card_SO = card_SOToDisplay; // card_SO has the data within the scriptable object of each card
        cardRankText.text = RankToString(card_SO.rank);
        cardScoreText.text = card_SO.score.ToString();
        SuitDisplay(card_SO.suit);
    }

    public string RankToString(Rank rank)
    {
        // Convert the Rank enum to a string representation
        return rank switch
        {
            Rank.Ace => "A",
            Rank.Two => "2",
            Rank.Three => "3",
            Rank.Four => "4",
            Rank.Five => "5",
            Rank.Six => "6",
            Rank.Seven => "7",
            Rank.Eight => "8",
            Rank.Nine => "9",
            Rank.Ten => "10",
            Rank.Jack => "J",
            Rank.Queen => "Q",
            Rank.King => "K",
            _ => throw new System.ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }

    public void SuitDisplay(Suit suit)
    {
        suitImage.sprite = suit switch
        {
            Suit.Clubs => clubsSPR,
            Suit.Hearts => heartsSPR,
            Suit.Spades => spadesSPR,
            Suit.Diamonds => diamondsSPR,
            _ => throw new System.ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }
}
