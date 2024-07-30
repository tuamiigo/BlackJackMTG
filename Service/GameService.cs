using BlackJackMTG.Interfaces;
using BlackJackMTG.Models;

namespace BlackJackMTG.Services
{
    public class GameService : IGameService
    {
        private readonly BlackjackRules _blackjackRules;
        private readonly Deck deck;
        private readonly List<Hand> playerHands;
        private Hand dealerHand;
        private int currentHandIndex;
        private bool isGameOver;
        public event Action? OnDealerHandUpdated;

        public GameService()
        {
            deck = new Deck();
            playerHands = new List<Hand>();
            dealerHand = new Hand();
            currentHandIndex = 0;
            isGameOver = false;
            _blackjackRules = new BlackjackRules();
            Console.WriteLine("GameService initialized.");
        }

        public void StartNewGame(int numberOfHands, List<decimal> betAmounts)
        {
            Console.WriteLine($"Starting new game with {numberOfHands} hands.");
            deck.Reset();
            deck.Shuffle();
            playerHands.Clear();
            dealerHand = new Hand();
            currentHandIndex = 0;
            isGameOver = false;

            for (int i = 0; i < numberOfHands; i++)
            {
                playerHands.Add(new Hand { Bet = betAmounts[i] });
            }

            DealInitialCards();
            Console.WriteLine("New game started.");
        }

        private void DealInitialCards()
        {
            Console.WriteLine("Dealing initial cards.");
            foreach (var hand in playerHands)
            {
                hand.AddCard(deck.DealCard());
                hand.AddCard(deck.DealCard());
                Console.WriteLine($"Player hand {playerHands.IndexOf(hand)}: {hand.ToString()}");
            }

            dealerHand.AddCard(deck.DealCard());
            dealerHand.AddCard(deck.DealCard());
            Console.WriteLine($"Dealer hand: {dealerHand.ToString()}");
        }

        public async void Hit()
        {
            if (!isGameOver && currentHandIndex < playerHands.Count)
            {
                var currentHand = playerHands[currentHandIndex];
                if (_blackjackRules.CanHit(currentHand))
                {
                    currentHand.AddCard(deck.DealCard());
                    Console.WriteLine($"Hit: Added card to hand {currentHandIndex}. New hand value: {currentHand.GetValue()}");

                    if (currentHand.IsBusted())
                    {
                        Console.WriteLine($"Hit: Hand {currentHandIndex} busted!");
                        await MoveToNextHand();
                    }
                }
                else
                {
                    Console.WriteLine("Hit: Game is over or invalid hand index.");
                }
            }
        }

        public async void Stand()
        {
            if (!isGameOver && currentHandIndex < playerHands.Count)
            {
                Console.WriteLine($"Stand: Moving to next hand from hand {currentHandIndex}.");
                await MoveToNextHand();
            }
            else
            {
                Console.WriteLine("Stand: Game is over or invalid hand index.");
            }
        }

        public async void DoubleDown()
        {
            if (!isGameOver && currentHandIndex < playerHands.Count)
            {
                var currentHand = playerHands[currentHandIndex];
                if (_blackjackRules.CanDoubleDown(currentHand))
                {
                    currentHand.Bet *= 2;
                    currentHand.AddCard(deck.DealCard());
                    Console.WriteLine($"DoubleDown: Added card to hand {currentHandIndex}. New hand value: {currentHand.GetValue()}");
                    await MoveToNextHand();
                }
                else
                {
                    Console.WriteLine($"DoubleDown: Cannot double down on hand {currentHandIndex}.");
                }
            }
            else
            {
                Console.WriteLine("DoubleDown: Game is over or invalid hand index.");
            }
        }

        public void Split()
        {
            if (!isGameOver && currentHandIndex < playerHands.Count)
            {
                var currentHand = playerHands[currentHandIndex];
                if (_blackjackRules.CanSplit(currentHand))
                {
                    Console.WriteLine("Splitting hand...");
                    var newHand = new Hand();
                    newHand.AddCard(currentHand.Cards[1]);
                    currentHand.Cards.RemoveAt(1);
                    playerHands.Insert(currentHandIndex + 1, newHand);

                    currentHand.AddCard(deck.DealCard());
                    newHand.AddCard(deck.DealCard());
                    Console.WriteLine("Hand split successfully.");
                }
            }
        }

        private async Task MoveToNextHand()
        {
            currentHandIndex++;
            if (currentHandIndex >= playerHands.Count)
            {
                await PlayDealerHand();
                DetermineWinners();
                isGameOver = true;
                OnDealerHandUpdated?.Invoke();
            }
        }

        public async Task PlayDealerHand()
        {
            Console.WriteLine("Dealer is playing...");
            OnDealerHandUpdated?.Invoke();
            await Task.Delay(1000);

            while (_blackjackRules.ShouldDealerHit(dealerHand))
            {
                dealerHand.AddCard(deck.DealCard());
                OnDealerHandUpdated?.Invoke();
                await Task.Delay(1000);
            }
            Console.WriteLine($"Dealer's final hand: {dealerHand.ToString()}");
        }

        private void DetermineWinners()
        {
            Console.WriteLine("Determining Winners.");
            foreach (var hand in playerHands)
            {
                hand.Outcome = _blackjackRules.DetermineOutcome(hand, dealerHand);
                decimal payout = 0;
                switch (hand.Outcome)
                {
                    case HandOutcome.Win:
                        payout = hand.IsBlackjack() ? hand.Bet * 1.5m : hand.Bet;
                        break;
                    case HandOutcome.Tie:
                        payout = 0;
                        break;
                    case HandOutcome.Lose:
                        payout = -hand.Bet;
                        break;
                }
                hand.Payout = payout;
                Console.WriteLine($"Player (Hand {playerHands.IndexOf(hand)}) {hand.Outcome}.");
            }
        }

        public List<Hand> GetPlayerHands()
        {
            return playerHands;
        }

        public Hand GetDealerHand()
        {
            return dealerHand;
        }

        public int GetCurrentHandIndex()
        {
            return currentHandIndex;
        }

        public Hand? GetCurrentHand()
        {
            if (currentHandIndex < playerHands.Count)
            {
                return playerHands[currentHandIndex];
            }
            else
            {
                return null;
            }
        }

        public bool IsGameOver()
        {
            return isGameOver;
        }
    }
}