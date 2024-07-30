using BlackJackMTG.Controllers;
using BlackJackMTG.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlackJackMTG.Components.Game
{
    public class GameBase : ComponentBase
    {
        protected List<decimal> betAmounts = new List<decimal>();
        protected int numberOfHands = 1;
        protected string errorMessage = "";
        protected bool showRules = false;
        protected bool isDealerTurn = false;
        protected bool showDealerSecondCard = false;

        [Inject] protected GameController? gameController { get; set; }
        [Inject] protected IJSRuntime? JSRuntime { get; set; } 

        protected override void OnInitialized()
        {
            UpdateBetAmounts();
            base.OnInitialized();
            gameController!.OnDealerHandUpdated += HandleDealerHandUpdated;
        }

        protected async void HandleDealerHandUpdated()
        {
            isDealerTurn = true;
            showDealerSecondCard = true;
            await InvokeAsync(StateHasChanged);

            if (gameController!.IsGameOver())
            {
                await Task.Delay(2000);
                isDealerTurn = false;
                showDealerSecondCard = false;
                await InvokeAsync(StateHasChanged);
            }
        }

        protected void UpdateBetAmounts()
        {
            if (betAmounts.Count < numberOfHands)
            {
                while (betAmounts.Count < numberOfHands)
                {
                    betAmounts.Add(0);
                }
            }
            else if (betAmounts.Count > numberOfHands)
            {
                betAmounts = betAmounts.Take(numberOfHands).ToList();
            }
        }

        protected void HandleBetAmountChange((ChangeEventArgs e, int index) args)
        {
            if (decimal.TryParse(args.e.Value!.ToString(), out var newBetAmount))
            {
                while (betAmounts.Count <= args.index)
                {
                    betAmounts.Add(0);
                }

                betAmounts[args.index] = newBetAmount;
                StateHasChanged();
            }
        }

        protected void OnNumberOfHandsChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value!.ToString(), out var newNumberOfHands))
            {
                if (newNumberOfHands >= 1 && newNumberOfHands <= 3)
                {
                    numberOfHands = newNumberOfHands;
                    errorMessage = "";
                    UpdateBetAmounts();
                }
                else
                {
                    numberOfHands = 1;
                    errorMessage = "Number of hands must be between 1 and 3.";
                    UpdateBetAmounts();
                    StateHasChanged();
                }
            }
        }

        protected void StartGame((int numHands, List<decimal> bets) args)
        {
            gameController!.StartNewGame(args.numHands, args.bets); 
        }

        protected void Hit(Hand hand)
        {
            if (hand != null)
            {
                gameController!.Hit();
                StateHasChanged();
            }
        }

        protected void Stand(Hand hand)
        {
            if (hand != null)
            {
                gameController!.Stand();
                StateHasChanged();
            }
        }

        protected void DoubleDown(Hand hand)
        {
            if (hand != null)
            {
                gameController!.DoubleDown();
                StateHasChanged();
            }
        }

        protected void Split(Hand hand)
        {
            if (hand != null)
            {
                gameController!.Split();
                StateHasChanged();
            }
        }

        protected bool CanDoubleDown(Hand hand)
        {
            return hand != null && hand.Cards.Count == 2;
        }

        protected bool CanSplit(Hand hand)
        {
            return hand != null && hand.Cards.Count == 2 && hand.Cards[0].CardRank == hand.Cards[1].CardRank;
        }

        protected decimal GetTotalBetAmount()
        {
            return betAmounts.Sum();
        }
    }
}
