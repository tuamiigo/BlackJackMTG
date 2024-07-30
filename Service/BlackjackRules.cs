using BlackJackMTG.Models;

namespace BlackJackMTG.Services {
    public class BlackjackRules {
        private const int DealerStandingThreshold = 17;
        
        public bool CanHit(Hand hand)
        {
            return !hand.IsBusted();
        }

        public bool CanDoubleDown(Hand hand) {
            return hand.Cards.Count == 2;
        }

        public bool CanSplit(Hand hand)
        {
                return hand.Cards.Count == 2 && hand.Cards[0].CardRank == hand.Cards[1].CardRank;
        }

        public bool ShouldDealerHit(Hand dealerHand) {
            return dealerHand.GetValue() < DealerStandingThreshold;
        }

        public HandOutcome DetermineOutcome(Hand playerHand, Hand dealerHand) {
            var playerValue = playerHand.GetValue();
            var dealerValue = dealerHand.GetValue();
            var playerBusted = playerHand.IsBusted();
            var dealerBusted = dealerHand.IsBusted();
            var playerHasBlackjack = playerHand.IsBlackjack();
            var dealerHasBlackjack = dealerHand.IsBlackjack();

            if (playerBusted)
            {
                return HandOutcome.Lose;
            }
            else if (dealerBusted)
            {
                return HandOutcome.Win;
            }
            else if (playerHasBlackjack && !dealerHasBlackjack)
            {
                return HandOutcome.Win;
            }
            else if (!playerHasBlackjack && dealerHasBlackjack)
            {
                return HandOutcome.Lose;
            }
            else if (playerValue > dealerValue)
            {
                return HandOutcome.Win;
            }
            else if (playerValue < dealerValue)
            {
                return HandOutcome.Lose;
            }
            else
            {
                return HandOutcome.Tie;
            }
        }
    }
}