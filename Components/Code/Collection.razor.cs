namespace BlackJackMTG.Components.Pages
{
    using System.Threading.Tasks.Dataflow;
    using BlackJackMTG.Models;
    using Microsoft.AspNetCore.Components;

    public partial class Collection
    {

        private IList<Card>? cards;
        private IList<Card>? filteredCardsSearch;
        private Card? selectedCard;
        private bool showPopup = false;

        private SearchModel searchModel = new SearchModel();
        [Inject]
        CollectionController? collectionController { get; set; }


        protected override async Task OnInitializedAsync()
        {
            if(collectionController == null)
            {
                return;
            }
            cards = await collectionController.GetCollection();
            filteredCardsSearch = cards;
        }

        private void ShowPopup(Card card)
        {
            selectedCard = card;
            showPopup = true;
        }

        private void MoreDetails(string id)
        {
            if(collectionController == null)
            {
                return;
            }
            collectionController.MoreDetails(id);
        }
        private void ClosePopup()
        {
            showPopup = false;
        }

        private void FilterCardsCost()
        {
            filteredCardsSearch = filteredCardsSearch?.OrderBy(c => c.ConvertedManaCost).ToList();
        }

        private void FilterCardsType()
        {
            filteredCardsSearch = filteredCardsSearch?.OrderBy(c => c.Type).ToList();
        }

        private void Search()
        {


            if (!string.IsNullOrWhiteSpace(searchModel.SearchTerm))
            {
                filteredCardsSearch = cards?.Where(c => c.Name.StartsWith(searchModel.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else
            {
                filteredCardsSearch = cards;
            }
        }



    }


}