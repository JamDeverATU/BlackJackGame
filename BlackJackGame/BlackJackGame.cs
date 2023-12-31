﻿//James Mccafferty Devers
//s00236260
//ObjectOrientedProgramming - 15%
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
                // Welcoming player to Blackjack.
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; // Set some color
                Console.WriteLine("Welcome to Blackjack!");
                Console.WriteLine("Max amount of chips is 999999999!!!!!");

                while (true)
                {
                    Console.Write("Enter the number of chips you want to buy-in: ");

                    if (int.TryParse(Console.ReadLine(), out int inputChips))
                    {
                        if (inputChips <= 999999999)
                        {
                            PlayerChips = inputChips;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount of chips. Please enter a value less than or equal to 999999999.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
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

                    // Validate and parse the entered bet.
                    if (int.TryParse(Console.ReadLine(), out int inputBet) && inputBet > 0 && inputBet <= PlayerChips)
                    {
                        // If the bet is valid, set the Bet property and exit the loop.
                        Bet = inputBet;
                        break;
                    }
                    else
                    {
                        // Display an error message and ask the player to try again.
                        Console.WriteLine("Invalid bet amount. Please try again.");
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

           public void DoubleDown()
            {
                // Check if the player has enough chips to double down.
                if (Bet * 2 > PlayerChips)
                {
                    Console.WriteLine("Not enough chips to double down. Choose another action.");
                    return;
                }

                // Double the bet.
                Bet *= 2;

                // Deal one more card to the player.
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
                Console.WriteLine($"Your hand after doubling down: {string.Join(", ", playerHand)}");

                // Check if the player busts (goes over 21).
                if (CalculateHandValue(playerHand) > 21)
                {
                    Console.WriteLine("Bust! You went over 21. Dealer wins.");
                    UpdateChips(-1); // Player loses.
                }
                else
                {
                    // Proceed with the dealer's turn: Reveal the hidden card and hit until at least 17 points.
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
            }
            

           public void Play()
            {
                while (PlayerChips > 0)
                {
                    PlaceBet();
                    DealInitialHands();

                    Console.WriteLine($"Your hand: {string.Join(", ", playerHand.Select(card => $"{card} (value {card.Value})"))}");

                    if (CalculateHandValue(playerHand) == 21)
                    {
                        Console.WriteLine("Congratulations! You got a natural blackjack!");
                        UpdateChips(1);
                    }
                    else
                    {
                        while (true)
                        {
                            Console.Write("Do you want to (H)it, (S)tand, or (D)ouble down? ");
                            string choice = Console.ReadLine().ToUpper();

                            if (choice == "H")
                            {
                                playerHand.Add(deck[0]);
                                deck.RemoveAt(0);

                                Console.WriteLine($"Your hand: {string.Join(", ", playerHand.Select(card => $"{card} (value {card.Value})"))}");

                                if (CalculateHandValue(playerHand) > 21)
                                {
                                    Console.WriteLine("Bust! You went over 21. Dealer wins.");
                                    UpdateChips(-1);
                                    break;
                                }
                            }
                            else if (choice == "S")
                            {
                                break;
                            }
                            else if (choice == "D")
                            {
                                DoubleDown();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid choice. Please enter 'H' for Hit, 'S' for Stand, or 'D' for Double Down.");
                            }
                        }

                        while (CalculateHandValue(dealerHand) < 17)
                        {
                            dealerHand.Add(deck[0]);
                            deck.RemoveAt(0);
                        }
                        Console.WriteLine($"Dealer's hand: {string.Join(", ", dealerHand.Select(card => $"{card} (value {card.Value})"))}");

                        int playerValue = CalculateHandValue(playerHand);
                        int dealerValue = CalculateHandValue(dealerHand);

                        if (dealerValue > 21 || playerValue > dealerValue)
                        {
                            Console.WriteLine("Congratulations! You win!");
                            UpdateChips(1);
                        }
                        else if (playerValue == dealerValue)
                        {
                            Console.WriteLine("It's a tie. Your bet is returned.");
                        }
                        else
                        {
                            Console.WriteLine("Dealer wins. You lose.");
                            UpdateChips(-1);
                        }
                    }

                    Console.WriteLine($"You have {PlayerChips} chips.");

                    // Check if the player has 0 chips, and exit the program if true.
                    if (PlayerChips == 0)
                    {
                        Console.WriteLine("You're out of chips! Thanks for playing Blackjack!");
                        Environment.Exit(0);
                    }

                    Console.Write("Play another round? (Y/N): ");
                    if (Console.ReadLine().ToUpper() != "Y") break;
                }

                Console.WriteLine("Thanks for playing Blackjack!");
            }

            
        }
    }
}
