﻿namespace BlackJackGame
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
                // Create a new Random object to generate random numbers for shuffling.
                Random random = new Random();

                // Get the number of cards in the deck.
                int n = deck.Count;

                // Start the Fisher-Yates shuffle algorithm.
                while (n > 1)
                {
                    // Decrease n by 1 in each iteration.
                    n--;

                    // Generate a random index (k) between 0 and the current value of n.
                    int k = random.Next(n + 1);

                    // Swap the card at index k with the card at index n.
                    Card value = deck[k];
                    deck[k] = deck[n];
                    deck[n] = value;
                }
            }

            public void DealInitialHands()
            {
                // Shuffle the deck to ensure randomness in card distribution.
                ShuffleDeck();

                // Clear the player's and dealer's hands to start with empty hands.
                playerHand.Clear();
                dealerHand.Clear();

                // Deal two cards to both the player and the dealer.
                for (int i = 0; i < 2; i++)
                {
                    // Add the first card from the shuffled deck to the player's hand and remove it from the deck.
                    playerHand.Add(deck[0]);
                    deck.RemoveAt(0);

                    // Add the first card from the shuffled deck to the dealer's hand and remove it from the deck.
                    dealerHand.Add(deck[0]);
                    deck.RemoveAt(0);
                }
            }

            public int CalculateHandValue(List<Card> hand)
            {
                // Initialize the variables to keep track of the total hand value and the number of Aces in the hand.
                int value = 0;
                int aces = 0;

                // Iterate through each card in the hand.
                foreach (Card card in hand)
                {
                    // Add the card's value to the total hand value.
                    value += card.Value;

                    // Check if the card is an Ace.
                    if (card.Rank == CardRank.Ace)
                        aces++;
                }

                // Check for a special case: if the hand's value is over 21 and there are Aces in the hand.
                while (value > 21 && aces > 0)
                {
                    // If the hand's value is over 21 and there's at least one Ace, treat one Ace as having a value of 1 instead of 11.
                    value -= 10;
                    aces--;
                }

                // Return the final calculated hand value, considering Aces' values.
                return value;
            }

            public void InitializeGame()
            {
                //Welcoming player to Blackjack.
                Console.WriteLine("Welcome to Blackjack!");
                Console.Write("Enter the number of chips you want to buy-in: ");
                PlayerChips = int.Parse(Console.ReadLine());
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

            public void UpdateChips(int result)
            {
                // The method updates the player's chip balance based on the game's result.

                // If the result is 1, it means the player won the game.
                if (result == 1)
                {
                    // Increase the player's chip balance by the amount they bet.
                    PlayerChips += Bet;
                }
                // If the result is -1, it means the player lost the game.
                else if (result == -1)
                {
                    // Decrease the player's chip balance by the amount they bet.
                    PlayerChips -= Bet;
                }
            }

            public void Play()
            {
                // The main game loop starts as long as the player has chips.
                while (PlayerChips > 0)
                {
                    // Allow the player to place a bet.
                    PlaceBet();
                    // Deal the initial hands for the player and dealer.
                    DealInitialHands();

                    // Display the player's hand and the dealer's visible card.
                    Console.WriteLine($"Your hand: {string.Join(", ", playerHand)}");
        
                    // Check if the player has a natural blackjack (21 with two cards).
                    if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.WriteLine("Congratulations! You got a natural blackjack!");
                        UpdateChips(1); // Player wins with a blackjack.
                    }
                    else
                    {
                        // The player's turn: Hit or Stand.
                        while (true)
                        {
                            Console.Write("Do you want to (H)it or (S)tand? ");
                            string choice = Console.ReadLine().ToUpper();

                            if (choice == "H")
                            {
                                // Player chooses to hit.
                                playerHand.Add(deck[0]);
                                deck.RemoveAt(0);
                                Console.WriteLine($"Your hand: {string.Join(", ", playerHand)}");

                                // Check if the player busts (goes over 21).
                                if (CalculateHandValue(playerHand) > 21)
                                {
                                    Console.WriteLine("Bust! You went over 21. Dealer wins.");
                                    UpdateChips(-1); // Player loses.
                                    break; // Exit the player's turn.
                                }
                            }
                            else if (choice == "S")
                            {
                                // Player chooses to stand, exit the player's turn.
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice. Please enter 'H' for Hit or 'S' for Stand.");
                            }
                        }

                        // The dealer's turn: Reveal the hidden card and hit until at least 17 points.
                        while (CalculateHandValue(dealerHand) < 17)
                        {
                            dealerHand.Add(deck[0]);
                            deck.RemoveAt(0);
                        }
                        Console.WriteLine($"Dealer's hand: {string.Join(", ", dealerHand)}");

                        // Determine the winner based on the hand values.
                        int playerValue = CalculateHandValue(playerHand);
                        int dealerValue = CalculateHandValue(dealerHand);

                        if (dealerValue > 21 || playerValue > dealerValue)
                        {
                            Console.WriteLine("Congratulations! You win!");
                            UpdateChips(1); // Player wins.
                        }
                        else if (playerValue == dealerValue)
                        {
                            Console.WriteLine("It's a tie. Your bet is returned.");
                        }
                        else
                        {
                            Console.WriteLine("Dealer wins. You lose.");
                            UpdateChips(-1); // Player loses.
                        }
                    }

                    // Display the player's remaining chips and ask if they want to play another round.
                    Console.WriteLine($"You have {PlayerChips} chips.");
                    Console.Write("Play another round? (Y/N): ");
                    if (Console.ReadLine().ToUpper() != "Y") break; // Exit the game loop if the player doesn't want to continue.
                }

                Console.WriteLine("Thanks for playing Blackjack!"); // End of the game.
            }
            
        }
    }
}
