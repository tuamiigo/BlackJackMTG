using System.Collections.Generic;

namespace BlackJackMTG.Models
{
    public class Deck {
        public  List<PlayingCard> Cards { get; private set;} 

        public Deck() {
            Cards = new List<PlayingCard>();
            Reset();
        }

        public void Reset() {
            Cards.Clear();
            foreach (PlayingCard.Suit suit in System.Enum.GetValues(typeof(PlayingCard.Suit))) {
                foreach(PlayingCard.Rank rank in System.Enum.GetValues(typeof(PlayingCard.Rank))) {
                    Cards.Add(new PlayingCard(suit, rank));
                }
            }   
        }

        public void Shuffle() {
            Random random = new Random();
            Cards = Cards.OrderBy(x => random.Next()).ToList();
        }

        public PlayingCard DealCard() {
            if(Cards.Count == 0) {
                Reset();
                Shuffle();
            }

            PlayingCard card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}