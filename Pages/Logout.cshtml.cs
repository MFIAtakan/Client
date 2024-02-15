// Logout.cshtml.cs
using Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            // Clear user session
            HttpContext.Session.Clear();
            var apiService = HttpContext.RequestServices.GetRequiredService<ApiService>();
            var result = apiService.LogoutUserAsync();
            if (result.Result.IsSuccess)
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/UserOperation");
        }
    }
}
