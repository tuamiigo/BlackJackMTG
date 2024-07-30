using BlackJackMTG.Models;

namespace BlackJackMTG.Interfaces
{
    public interface IGameService
    {
        void StartNewGame(int numberOfHands, List<decimal> betAmounts);
        void Hit();
        void Stand();
        void DoubleDown();
        void Split();
        List<Hand> GetPlayerHands();
        Hand GetDealerHand();
        int GetCurrentHandIndex();
        Hand? GetCurrentHand();
        bool IsGameOver();
        Task PlayDealerHand();
        event Action OnDealerHandUpdated;
    }
}