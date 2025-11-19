using Front.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Front.Pages
{
    public class ResponsableModel : PageModel
    {
        private readonly ILogger<ResponsableModel> _logger;
        private readonly HttpClient _httpClient;

        public ResponsableModel(ILogger<ResponsableModel> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient(); // instancia directa
        }

        public List<Area> lAreas { get; set; } = new List<Area>();

        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("http://localhost:8082/area");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                lAreas = System.Text.Json.JsonSerializer.Deserialize<List<Area>>(json,
                    new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<IActionResult> OnPost(string Nombre, string Correo, int area)
        {
            var request = new ResponsableRequest
            {
                nombre = Nombre,
                correo = Correo,
                idarea = area
            };

            var json = System.Text.Json.JsonSerializer.Serialize(request);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8082/responsable", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Responsable insertado correctamente";
                return RedirectToPage(); // hace un GET nuevo y TempData se consume
            }
            else
            {
                TempData["Message"] = "Error al insertar responsable";
                return RedirectToPage();
            }
        }
    }
}
