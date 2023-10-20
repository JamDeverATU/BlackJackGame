using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Define an enumeration for card ranks, representing the values of playing cards.
public enum CardRank
{
    Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
}

// Define an enumeration for card suits, representing the different suits of playing cards.
public enum CardSuit
{
    Hearts, Diamonds, Clubs, Spades
}

namespace BlackJackGame
{
    internal class Card
    {
        // These properties define the rank and suit of the card.
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }

        // This property calculates and returns the value of the card.
        public int Value
        {
            get
            {
                if (Rank == CardRank.Ace)
                    return 11; // Ace can be worth 11 points.
                if (Rank >= CardRank.Ten)
                    return 10; // Face cards (King, Queen, Jack) are worth 10 points.
                return (int)Rank + 2; // Other cards have values based on their rank.
            }
        }

        // This method provides a string representation of the card.
        public override string ToString()
        {
            return $"{Rank} of {Suit}"; // Returns a string like "King of Hearts".
        }
    }
}
