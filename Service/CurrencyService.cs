public class CurrencyService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CurrencyKey = "CurrencyBalance";

    public CurrencyService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddCurrency(int amount)
    {
        await Task.Run(() =>
    {
        var currentBalance = GetCurrencyBalance();
        currentBalance += amount;
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32(CurrencyKey, currentBalance);
        }
    });
    }

    public bool SubtractCurrency(int amount)
    {
        var currentBalance = GetCurrencyBalance();
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null && amount <= currentBalance)
        {
            currentBalance -= amount;
            _httpContextAccessor.HttpContext.Session.SetInt32(CurrencyKey, currentBalance);
            return true;
        }
        return false;
    }

    public int GetCurrencyBalance()
    {
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
        {
            return _httpContextAccessor.HttpContext.Session.GetInt32(CurrencyKey) ?? 0;
        }
        return 0;
    }

    public async Task SetCurrencyBalance()
    {
        if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Session != null)
        {
            await _httpContextAccessor.HttpContext.Session.SetInt32Async(CurrencyKey, 50);
        }
    }
}
