using Microsoft.AspNetCore.Components;

namespace BlackJackMTG.Components.Game
{
    public partial class GameControls
    {
        [Parameter] public int NumberOfHands { get; set; }
        [Parameter] public List<decimal> BetAmounts { get; set; } = new List<decimal>(); 
        [Parameter] public EventCallback<(int, List<decimal>)> StartGame { get; set; }
        [Parameter] public EventCallback<(ChangeEventArgs, int)> HandleBetAmountChange { get; set; }
        [Parameter] public EventCallback<ChangeEventArgs> OnNumberOfHandsChanged { get; set; }
    }
}