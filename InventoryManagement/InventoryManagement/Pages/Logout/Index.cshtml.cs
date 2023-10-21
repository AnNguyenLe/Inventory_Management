using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InventoryManagement.Pages.Logout
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.SetString("userEmail", string.Empty);
            HttpContext.Session.SetString("userName", string.Empty);

            Response.Redirect("/login");
        }
    }
}
