using Microsoft.AspNetCore.Components;

namespace BlackJackMTG.Components.Pages
{


    public partial class Home
    {
        [Inject]
        private HomeController? controller { get; set; }

        public int CurrentBalance => controller?.CurrentBalance ?? 0;

        private async Task AddMoney()
        {
            if (controller == null)
            {
                return;
            }
            await controller.AddMoney();
        }
    }

}