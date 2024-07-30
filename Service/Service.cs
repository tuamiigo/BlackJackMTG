using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlackJackMTG.Models;
namespace BlackJackMTG.Services
{
    public class CardService
    {
        private readonly PostgresContext _dbContext;

        public CardService(PostgresContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Card>> GetRandomCards()
        {

            var cards = await _dbContext.Cards
        .Where(p => p.OriginalImageUrl != null && p.ConvertedManaCost != "0")
        .ToListAsync();

            var randomCards = cards
                .OrderBy(a => Guid.NewGuid())
                .Take(9)
                .Select(p => new Card
                {
                    MtgId = p.MtgId,
                    OriginalImageUrl = p.OriginalImageUrl,
                    Name = p.Name,
                    ConvertedManaCost = p.ConvertedManaCost,
                    Text = p.Text,
                    Type = p.Type
                })
                .ToList();

            return randomCards;
        }

        public async Task<Card?> GetCardById(string id)
        {
            var card = await _dbContext.Cards
                .Where(p => p.MtgId == id)
                .Select(p => new Card
                {
                    Id = p.Id,
                    OriginalImageUrl = p.OriginalImageUrl,
                    Name = p.Name,
                    ConvertedManaCost = p.ConvertedManaCost,
                    Text = p.Text,
                    Type = p.Type,
                    RarityCodeNavigation = p.RarityCodeNavigation,
                })
                .FirstOrDefaultAsync();

            return card;
        }

    }
}
