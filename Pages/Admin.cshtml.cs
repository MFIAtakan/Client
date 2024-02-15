using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Client.Services;
using Client.Models;

namespace Client.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ApiService _apiService;

        public AdminModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public List<UserOperationInfoModel> UserOperations { get; set; }

        public async Task OnGet()
        {
            var apiResponse = await _apiService.GetUsersInfoAsync();

            if (apiResponse.IsSuccess)
            {
                UserOperations = apiResponse.Result;
            }
            else
            {
                // Handle error, you might want to log or display an error message
            }
        }
    }
}
