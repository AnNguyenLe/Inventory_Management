using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagement.Pages.Success
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ActionType { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RedirectTo { get; set; }

        public IndexModel(): base() {
            ActionType = string.Empty;
            RedirectTo = string.Empty;
        }
        public void OnGet()
        {
            Console.WriteLine(ActionType);
            Console.WriteLine(RedirectTo);
            Console.WriteLine();
        }
    }
}
