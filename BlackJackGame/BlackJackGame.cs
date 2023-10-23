using BlackJackGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackGame
{

    internal class BlackJackGame
    {
        public class BlackjackGame
        {
            private List<Card> deck;
            private List<Card> playerHand;
            private List<Card> dealerHand;

            public int PlayerChips { get; set; }
            public int Bet { get; set; }

            public BlackjackGame()
            {
                InitializeDeck();
                playerHand = new List<Card>();
                dealerHand = new List<Card>();
            }

            public void InitializeDeck()
            {
                deck = new List<Card>();
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    {
                        deck.Add(new Card { Suit = suit, Rank = rank });
                    }
                }
            }

        }
    }

}
