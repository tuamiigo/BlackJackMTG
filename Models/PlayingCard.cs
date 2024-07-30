namespace BlackJackMTG.Models
{
    public class PlayingCard {
        public enum Suit {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public enum Rank {
            Ace = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13
        }

        public Suit CardSuit { get; set; }
        public Rank CardRank { get;set; }

        public PlayingCard (Suit suit, Rank rank) {
            CardSuit = suit;
            CardRank = rank;
        }

        public string ImageName {
            get
            {
                string suitInitial = CardSuit.ToString().Substring(0, 1);
                string rankValue;

                switch (CardRank)
                {
                    case Rank.Ten:
                        rankValue = "T";
                        break;
                    case Rank.Jack:
                        rankValue = "J";
                        break;
                    case Rank.Queen:
                        rankValue = "Q";
                        break;
                    case Rank.King:
                        rankValue = "K";
                        break;
                    case Rank.Ace:
                        rankValue = "A";
                        break;
                    default:
                        rankValue = ((int)CardRank).ToString();
                        break;
                }

                return $"{rankValue}{suitInitial}.svg";
            }
        }
        
        public override string ToString()
        {
            return $"{CardRank} of {CardSuit}";
        }
    }
}