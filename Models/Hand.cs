namespace BlackJackMTG.Models {
    public enum HandOutcome {
        Pending,
        Win,
        Lose,
        Tie
    }

    public class Hand {
        public List<PlayingCard> Cards { get; private set; }
        public bool IsSplit { get; set; }
        public HandOutcome Outcome { get; set; } = HandOutcome.Pending;
        private const int FaceCardValue = 10;
        private const int SoftHandThreshold = 11;
        private const int BlackjackValue = 21;
        public decimal Bet { get; set; }
        public decimal Payout { get; set; }
        public Hand() {
            Cards = new List<PlayingCard>();
            IsSplit = false;
        }

        public void AddCard(PlayingCard card) {
            Cards.Add(card);
        }

        public int GetValue() {
            int value = Cards.Sum(c => (int)c.CardRank <= FaceCardValue ? (int)c.CardRank : FaceCardValue);
            int numAces = Cards.Count(c => c.CardRank == PlayingCard.Rank.Ace);

            while (numAces > 0 && value <= SoftHandThreshold){
                value += 10;
                numAces--;
            }

            return value;
        }

        public int GetCardValue(int index) {
            if (index < 0 || index >= Cards.Count) {
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the Cards list.");
            }

            var card = Cards[index];
            if (card.CardRank == PlayingCard.Rank.Ace) {
                return 11;
            }
            else if ((int)card.CardRank > 10) {
                return 10;
            }
            else {
                return (int)card.CardRank;
            }
        }

        public bool IsBusted() {
            return GetValue() > 21;
        }

        public bool IsBlackjack() {
            return Cards.Count == 2 && GetValue() == BlackjackValue;
        }
    }
}