using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Pages
{
    public class UserOperationModel : PageModel
    {
        public string Result { get; set; }
        [BindProperty]
        public List<int> EnteredValues { get; set; }


        public IActionResult OnGet()
        {
            Result = HttpContext.Request.Query["result"];
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var list = Request.Form["EnteredValues"].ToString(); // Get the JSON string
            EnteredValues = JsonSerializer.Deserialize<List<int>>(list); // Deserialize JSON string to List<int>

            var apiService = HttpContext.RequestServices.GetRequiredService<ApiService>();

            UserOperationRequestModel model = new UserOperationRequestModel
            {
                Entries = EnteredValues
            };

            var result = await apiService.HandleUserOperation(model);

            if (result.IsSuccess)
            {
                return RedirectToPage("/UserOperation");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
