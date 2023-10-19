using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Login;

namespace InventoryManagement.Pages.Login
{
    public class IndexModel : PageModel
    {
        private ILoginService _service;
        [BindProperty]
        public string UserEmail { get; set; }
        [BindProperty]
        public string UserPassword { get; set; }
        public string ErrorMessage { get; set; }
        public string Info {  get; set; }

        public IndexModel(string userEmail = "", string userPassword = "", string errorMessage = "", string info = "") : base()
        {
            _service = ServiceInstances.LoginService;
            UserEmail = userEmail;
            UserPassword = userPassword;
            ErrorMessage = errorMessage;
            Info = info;
        }

        public void OnPost()
        {
            var result = _service.Login(UserEmail, UserPassword);
            if(result.isSuccessful && result.Data is not null)
            {
                HttpContext.Session.SetString("userEmail", UserEmail);
                HttpContext.Session.SetString("userName", result.Data.UserName);
                Response.Redirect("/");
            }
            ErrorMessage = result.ErrorMessage;
        }
    }
}