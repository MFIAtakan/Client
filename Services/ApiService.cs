using Client.Models;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Client.Services
{
    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<object>> AuthenticateUserAsync(LoginRequestModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44365/api/user/login", content);

            var apiResponse = new ApiResponse<object>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                apiResponse.Result = await response.Content.ReadFromJsonAsync<object>();
                // Retrieve user information from the response and store it in session
                _httpContextAccessor.HttpContext.Session.SetString("UserName", apiResponse.Result.ToString());
                _httpContextAccessor.HttpContext.Session.SetString("UserId", apiResponse.Result.ToString());

            }
            else
            {
                apiResponse.ErrorMessages.Add("Authentication failed.");
            }

            return apiResponse;
        }

        public async Task<ApiResponse<object>> HandleUserOperation(UserOperationRequestModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44365/api/operation/submitValues", content);

            var apiResponse = new ApiResponse<object>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                apiResponse.Result = await response.Content.ReadFromJsonAsync<object>();
            }
            else
            {
                apiResponse.ErrorMessages.Add("Operasyon başarısız."); 
            }

            return apiResponse;
        }

        public async Task<ApiResponse<object>> RegisterUserAsync(RegisterRequestModel model)
            {
            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://localhost:44365/api/user/register", content);

            var apiResponse = new ApiResponse<object>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (!response.IsSuccessStatusCode)
            {
                apiResponse.ErrorMessages.Add("Registration failed.");
            }

            return apiResponse;
        }

        public async Task<ApiResponse<object>> LogoutUserAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();


            var response = await httpClient.GetAsync("https://localhost:44365/api/user/logout");

            var apiResponse = new ApiResponse<object>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (!response.IsSuccessStatusCode)
            {
                apiResponse.ErrorMessages.Add("Logout failed.");
            }

            return apiResponse;
        }

        public async Task<ApiResponse<List<UserOperationInfoModel>>> GetUsersInfoAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetAsync("https://localhost:44365/api/operation/GetUsersInfo");

            var apiResponse = new ApiResponse<List<UserOperationInfoModel>>
            {
                StatusCode = response.StatusCode,
                IsSuccess = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    // Deserialize the entire response object
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse<List<UserOperationInfoModel>>>(content);

                    // Assign the properties from the response object to the apiResponse
                    apiResponse.StatusCode = responseObject.StatusCode;
                    apiResponse.IsSuccess = responseObject.IsSuccess;
                    apiResponse.ErrorMessages = responseObject.ErrorMessages;
                    apiResponse.Result = responseObject.Result;
                }
                catch (JsonReaderException)
                {
                    // If deserialization fails, set the Result as null
                    apiResponse.Result = null;
                }
            }
            else
            {
                apiResponse.ErrorMessages.Add("Failed to retrieve user operations.");
            }

            return apiResponse;
        }


    }
}
