using BlackJackMTG.Models;
using BlackJackMTG.Services;

public class CardDetailsController 
{
    private readonly CardService cardService;

    public CardDetailsController(CardService cardService)
    {
        this.cardService = cardService;
    }

    public async Task<Card?> GetCardById(string id)
    {
        if (cardService != null)
        {
            return await cardService.GetCardById(id);
        }
        else
        {
            return null;
        }
    }
}