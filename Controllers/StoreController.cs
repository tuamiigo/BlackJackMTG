using BlackJackMTG.Services;
using BlackJackMTG.Models;

public class StoreController 
{
    private readonly CardService cardService;
    private readonly CurrencyService currencyService;
    private readonly CollectionService collectionService;

    public StoreController(CardService cardService, CurrencyService currencyService, CollectionService collectionService)
    {
        this.cardService = cardService;
        this.currencyService = currencyService;
        this.collectionService = collectionService;
    }

    public int CurrentBalance => currencyService.GetCurrencyBalance();

    public async Task<string> SubstractMoney(Card card)
    {
        currencyService.SubtractCurrency(Convert.ToInt16(card.ConvertedManaCost));
        await collectionService.AddCard(card);
        
        return "successful"; 
    }

    public async Task<IList<Card>> GetCards()
    {
         return await cardService.GetRandomCards();
        
    }
}