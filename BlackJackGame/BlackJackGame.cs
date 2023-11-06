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

            public void InitializeGame()
            {
                //Welcoming player to Blackjack.
                Console.WriteLine("Welcome to Blackjack!");
                Console.Write("Enter the number of chips you want to buy-in: ");
                PlayerChips = int.Parse(Console.ReadLine());
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

            public void PlaceBet()
            {
                // This method allows the player to place a bet in a game.

                while (true)
                {
                    // Display the number of chips the player currently has.
                    Console.WriteLine($"You have {PlayerChips} chips.");

                    // Prompt the player to enter their bet.
                    Console.Write("Enter your bet: ");
                    Bet = int.Parse(Console.ReadLine());

                    // Check if the entered bet is invalid (less than or equal to 0 or more than the player's chips).
                    if (Bet <= 0 || Bet > PlayerChips)
                    {
                        // Display an error message and ask the player to try again.
                        Console.WriteLine("Invalid bet amount. Please try again.");
                    }
                    else
                    {
                        // If the bet is valid, exit the loop and proceed.
                        break;
                    }
                }
            }

            public void ShuffleDeck()
            {
                // Create a new Random object to generate random numbers.
                Random random = new Random();

                // Get the number of cards in the deck.
                int n = deck.Count;

                while (n > 1)
                {
                    n--;

                    // Generate a random index (k) between 0 and the current value of n.
                    int k = random.Next(n + 1);

                    // Swap the card at index k with the card at index n.
                    Card value = deck[k];
                    deck[k] = deck[n];
                    deck[n] = value;
                }
            }









            public void Play()
            {
            }





























            }
    }

}
