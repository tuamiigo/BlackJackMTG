using BlackJackMTG.Models;
using Microsoft.AspNetCore.Components;
using BlackJackMTG.Controllers;

namespace BlackJackMTG.Components.Game
{
    public partial class GameOver
    {
        [Parameter] public List<Hand> PlayerHands { get; set; } = new List<Hand>();
        [Parameter] public Hand DealerHand { get; set; } = new Hand(); 
        [Parameter] public int NumberOfHands { get; set; }
        [Parameter] public List<decimal> BetAmounts { get; set; } = new List<decimal>();
        [Parameter] public EventCallback<(int, List<decimal>)> StartGame { get; set; }
        [Parameter] public EventCallback<(ChangeEventArgs, int)> HandleBetAmountChange { get; set; }
        [Parameter] public EventCallback<ChangeEventArgs> OnNumberOfHandsChanged { get; set; }
        [Parameter] public string errorMessage { get; set; } = string.Empty; 

        private bool showGameControls = false;

        private void ShowGameControls()
        {
            showGameControls = true;
        }

        protected override async Task OnInitializedAsync()
        {

            gameController.ChangeBalance(PlayerHands.Sum(x => x.Payout));

        }

    }
}