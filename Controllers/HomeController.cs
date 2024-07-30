

public class HomeController
{
    private readonly CurrencyService currencyService;

     public HomeController(CurrencyService currencyService)
    {
        this.currencyService = currencyService;
    }

    public int CurrentBalance => currencyService.GetCurrencyBalance();


    public async Task<string> AddMoney()
{
    await currencyService.SetCurrencyBalance();
    return "successful";
}
    
}