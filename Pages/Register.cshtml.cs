using System.Threading.Tasks;
using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterRequestModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var apiService = HttpContext.RequestServices.GetRequiredService<ApiService>();

                var registrationResult = await apiService.RegisterUserAsync(Input);

                if (registrationResult.IsSuccess)
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    foreach (var error in registrationResult.ErrorMessages)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }

                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
