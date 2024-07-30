using Microsoft.AspNetCore.Components;

namespace BlackJackMTG.Components.Pages
{


    public partial class Store
    {

        private Models.Card? selectedCard;
        private bool showPopup = false;
        private IList<Models.Card>? cards;
        [Inject]
        StoreController? storeController { get; set; }
        public int CurrentBalance => storeController?.CurrentBalance ?? 0;

        protected override async Task OnInitializedAsync()
        {
            if (storeController == null)
            {
                return;
            }

            cards = await storeController.GetCards();

        }

        void ShowPopup(Models.Card card)
        {
            selectedCard = card;
            showPopup = true;

        }

        void ClosePopup()
        {
            showPopup = false;
        }

        void FilterCardsCost()
        {
            if (cards == null)
            {
                return;
            }
            cards = cards.OrderBy(c => c.ConvertedManaCost).ToList();
        }

        void FilterCardsType()
        {
            if (cards == null)
            {
                return;
            }
            cards = cards.OrderBy(c => c.Type).ToList();
        }

        async Task BuyCard(Models.Card card)
        {
            if (card == null || CurrentBalance < Convert.ToInt16(card.ConvertedManaCost))
            {
                return;
            }
            if (storeController == null)
            {
                return;
            }
            await storeController.SubstractMoney(card);
        }

    }


}