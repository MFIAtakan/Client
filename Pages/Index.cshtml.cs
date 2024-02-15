using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;

public class IndexModel : PageModel
{

    [BindProperty]
    public LoginRequestModel Input { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (ModelState.IsValid)
        {
            var apiService = HttpContext.RequestServices.GetRequiredService<ApiService>();

            var authenticationResult = await apiService.AuthenticateUserAsync(Input);

            if (authenticationResult.IsSuccess)
            {
                dynamic resultObject = JsonConvert.DeserializeObject(authenticationResult.Result.ToString());

                var rolesArray = resultObject.result?.roles as JArray;
                var rolesList = rolesArray?.ToObject<List<string>>();

                if (rolesList != null && rolesList.Contains("user"))
                {
                    return RedirectToPage("/UserOperation", new { result = authenticationResult.Result.ToString() });
                }
                else
                {
                    return RedirectToPage("/Admin", new { result = authenticationResult.Result.ToString() });
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz giriþ.");
                return Page();
            }
        }
        else
        {
            return Page();
        }
    }
}
