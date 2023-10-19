
using Entities;
using InventoryManagement.ObjectCreators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Register;

namespace InventoryManagement.Pages.Register
{
    public class IndexModel : PageModel
    {
        private IRegisterService _service;
        [BindProperty]
        public string UserEmail { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string UserPassword { get; set; }
        public string ErrorMessage { get; private set; }
        public string Info {  get; private set; }
        public bool IsRegisterSuccessul {  get; private set; }

        public IndexModel(string userEmail = "", string userName = "", string userPassword = "", string errorMessage = "", string info = "") : base()
        {
            _service = ServiceInstances.RegisterService;
            UserEmail = userEmail;
            UserName = userName;
            UserPassword = userPassword;
            ErrorMessage = errorMessage;
            Info = info;
        }
        public void OnPost()
        {
            try
            {
                User newUser = new(UserEmail, UserName, UserPassword);
                var result = _service.Register(newUser);

                if(result.isSuccessful)
                {
                    Info = "Register successfully!";
                    IsRegisterSuccessul = true;

                    HttpContext.Session.SetString("userEmail", UserEmail);
                    HttpContext.Session.SetString("userName", UserName);

                    Response.Redirect("/");
                } else
                {
                    ErrorMessage = result.ErrorMessage;
                }
            } 
            catch(Exception)
            {
                Response.Redirect("/Error");
            }
        }
    }
}
