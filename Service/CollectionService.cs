using BlackJackMTG.Models;


public class CollectionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CollectionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task AddCard(Card card)
    {
        var collection = await GetCollection();
        collection.Add(card);
        if (_httpContextAccessor.HttpContext != null)
        {
            _httpContextAccessor.HttpContext.Session?.SetObject("Collection", collection);
        }
    }
    public async Task<List<Card>> GetCollection()
    {
        return await Task.Run(() =>
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                return _httpContextAccessor.HttpContext.Session?.GetObject<List<Card>>("Collection") ?? new List<Card>();
            }
            return new List<Card>();
        });
    }

}


