using BlackJackMTG.Models;
using Microsoft.AspNetCore.Components;

namespace BlackJackMTG.Components.Game
{
    public partial class PlayerHands
    {
        [Parameter] public List<Hand> Hands { get; set; } = new List<Hand>(); 
        [Parameter] public Hand CurrentHand { get; set; } = new Hand(); 
        [Parameter] public EventCallback<Hand> Hit { get; set; }
        [Parameter] public EventCallback<Hand> Stand { get; set; }
        [Parameter] public EventCallback<Hand> DoubleDown { get; set; }
        [Parameter] public EventCallback<Hand> Split { get; set; }
        [Parameter] public Func<Hand, bool> CanDoubleDown { get; set; } = hand => false; 
        [Parameter] public Func<Hand, bool> CanSplit { get; set; } = hand => false;

        private bool CanPlayCurrentHand => CurrentHand != null;
    }
}