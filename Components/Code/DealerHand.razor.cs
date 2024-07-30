using BlackJackMTG.Models;
using Microsoft.AspNetCore.Components;

namespace BlackJackMTG.Components.Game
{
    public partial class DealerHand
    {
        [Parameter] public Hand Hand { get; set; } = new Hand();
        [Parameter] public bool IsDealerTurn { get; set; }
        [Parameter] public bool ShowDealerSecondCard { get; set; }
    }
}