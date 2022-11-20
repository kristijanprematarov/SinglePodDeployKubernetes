using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestApp.WebApi.Models;

namespace TestApp.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.RequestUri = new Uri("http://localhost:81/api/jokes");
                var response = await client.SendAsync(request);

                var joke = await response.Content.ReadAsStreamAsync();

                ViewData["Joke"] = await JsonSerializer.DeserializeAsync<Joke>(joke, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return Page();
            }
        }
    }
}
