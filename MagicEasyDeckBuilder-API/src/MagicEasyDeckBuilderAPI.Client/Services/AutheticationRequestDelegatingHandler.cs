using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace MagicEasyDeckBuilderAPI.Client.Services
{
    public class AutheticationRequestDelegatingHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;

        public AutheticationRequestDelegatingHandler(ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _localStorage.GetItemAsStringAsync("token");

            request.Headers.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);

                if (string.IsNullOrEmpty(returnUrl))
                    _navigationManager.NavigateTo($"/login");
                else
                    _navigationManager.NavigateTo($"/login?returnUrl={returnUrl}");

            }

            return response;
        }
    }
}
