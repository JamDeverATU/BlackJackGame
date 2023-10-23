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
                // Create a new list to represent the deck of cards.
                deck = new List<Card>();

                // Loop through all possible card suits using Enum.GetValues.
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    // Loop through all possible card ranks using Enum.GetValues.
                    foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                    {
                        // Create a new card object and add it to the deck.
                        deck.Add(new Card { Suit = suit, Rank = rank });
                    }
                }
            }
            public void ShuffleDeck()
            {
                // Create a Random object to generate random numbers.
                Random random = new Random();

                // Iterate through the deck in reverse order.
                for (int j = deck.Count - 1; j > 0; j--)
                {
                    // Generate a random index between 0 and j (inclusive).
                    int k = random.Next(j + 1);

                    // Swap the cards at indices j and k.
                    (deck[j], deck[k]) = (deck[k], deck[j]);
                }
            }





























        }
    }

}
