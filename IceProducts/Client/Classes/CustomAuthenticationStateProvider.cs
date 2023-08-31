using Blazored.LocalStorage;
using IceProducts.Shared.Responses;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace IceProducts.Client.Classes
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
             //if it's empty then the user is not authenticated
            ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());

            var tokenObject = await _localStorage.GetItemAsync<AccessToken>("AccessTokenObject");


            if (tokenObject == null || string.IsNullOrEmpty(tokenObject.Token))
            {
                return new AuthenticationState(_anonymousUser);
            }

            //create a claim prinicipal for the valid client
            ClaimsPrincipal claimingUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(tokenObject.Token), "jwtAuth"));
            return new AuthenticationState(claimingUser);
        }

        public async Task SetAuthenticationState(AccessToken accessResponse)
        {
            await _localStorage.SetItemAsync("AccessTokenObject", accessResponse);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task RemoveAuthenticationState(string key)
        {
            await _localStorage.RemoveItemAsync(key);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
