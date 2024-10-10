using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DemoWASM.Pages.Demos.Auth
{
    public partial class Login
    {
        public LoginForm form { get; set; } = new LoginForm();

        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        public async Task SubmitForm()
        {
            HttpResponseMessage response = 
                await Client.PostAsJsonAsync("user/login", form);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync("Erreur : " + response.ReasonPhrase);
            }

            string token = await response.Content.ReadAsStringAsync();
            await JS.InvokeVoidAsync("localStorage.setItem", "token", token);

        }
    }
}
