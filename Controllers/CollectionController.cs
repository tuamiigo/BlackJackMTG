using BlackJackMTG.Models;
using Microsoft.AspNetCore.Components;

public class CollectionController 
{
    private readonly CollectionService collectionService;
    private readonly NavigationManager navigationManager;

    public CollectionController(CollectionService collectionService, NavigationManager navigationManager)
    {
        this.collectionService = collectionService;
        this.navigationManager = navigationManager;
    }

    public async Task<IList<Card>> GetCollection()
    {
        return await Task.Run(collectionService.GetCollection);
    }

    public Task<string> MoreDetails(string id)
    {
        navigationManager?.NavigateTo($"/carddetails/{id}", true);
        return Task.FromResult("successful");
    }

}