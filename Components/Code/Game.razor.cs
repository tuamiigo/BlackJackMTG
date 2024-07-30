using BlackJackMTG.Controllers;
using BlackJackMTG.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlackJackMTG.Components.Game
{
    public partial class Game : GameBase
    {
        [Inject] new private GameController? gameController { get; set; } 
        [Inject] new private IJSRuntime? JSRuntime { get; set; }

    }
}
