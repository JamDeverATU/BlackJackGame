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
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }

       public int value
        {
            get
            {
                if (Rank == CardRank.Ace)
                    return 11;
                if (Rank >= CardRank.Ten) 
                    return 10;
                return (int)Rank+2;
            }
        }
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }

    }
}
