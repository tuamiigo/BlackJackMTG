namespace BlackJackMTG.Components.Pages
{
    using Microsoft.AspNetCore.Components;
    public partial class CardDetails
    {

        [Parameter]
        public string? MtgId { get; set; }

        private Models.Card? card;
        [Inject]
        CardDetailsController? cardDetailsController { get; set; }



        protected override async Task OnInitializedAsync()
        {
            if(cardDetailsController == null || MtgId == null)
            {
                return;
            }
            
            var result = await cardDetailsController.GetCardById(MtgId);
            if (result is Models.Card cardResult)
            {
                card = cardResult;
            }
        }
    }

}