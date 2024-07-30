using BlackJackMTG.Interfaces;
using BlackJackMTG.Models;

namespace BlackJackMTG.Controllers
{
    public class GameController
    {
        private readonly IGameService _gameService;
        private readonly CurrencyService currencyService;

        public GameController(IGameService gameService, CurrencyService currencyService)
        {
            _gameService = gameService;
            this.currencyService = currencyService;
        }

        public int CurrentBalance => currencyService.GetCurrencyBalance();

        public void StartNewGame(int numberOfHands, List<decimal> betAmounts)
        {
            _gameService.StartNewGame(numberOfHands, betAmounts);
        }

        public void Hit()
        {
            _gameService.Hit();
        }

        public void Stand()
        {
            _gameService.Stand();
        }

        public void DoubleDown()
        {
            _gameService.DoubleDown();
        }

        public void Split()
        {
            _gameService.Split();
        }

        public List<Hand> GetPlayerHands()
        {
            return _gameService.GetPlayerHands();
        }

        public Hand GetDealerHand()
        {
            return _gameService.GetDealerHand();
        }

        public Hand GetCurrentHand(){
            return _gameService.GetCurrentHand()!;
        }

        public bool IsGameOver()
        {
            return _gameService.IsGameOver();
        }

        public event Action OnDealerHandUpdated
        {
            add { _gameService.OnDealerHandUpdated += value; }
            remove { _gameService.OnDealerHandUpdated -= value; }
        }
        public void ChangeBalance(decimal amount)
        {
            int roundedAmount = (int)Math.Round(amount, MidpointRounding.AwayFromZero);
            currencyService.AddCurrency(roundedAmount);
        }
   
    }
}
