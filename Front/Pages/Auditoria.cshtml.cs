using Front.Model;
using Front.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Front.Pages
{
    public class AuditoriaModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public AuditoriaModel()
        {
            _httpClient = new HttpClient();
        }

        // Propiedades para binding desde el formulario
        [BindProperty] public string Titulo { get; set; }
        [BindProperty] public int Area { get; set; }
        [BindProperty] public int Responsable { get; set; }
        [BindProperty] public DateTime FechaInicio { get; set; }
        [BindProperty] public DateTime? FechaFin { get; set; }
        [BindProperty] public int Estado { get; set; }

        public List<Area> lAreas { get; set; } = new List<Area>();
        public List<Responsable> lResponsables { get; set; } = new List<Responsable>();
        public List<Estado> lEstados { get; set; } = new List<Estado>();

        public async Task OnGet()
        {
            // Aquí llamas a tus APIs para llenar las listas
            var responseAreas = await _httpClient.GetAsync("http://localhost:8082/area");
            if (responseAreas.IsSuccessStatusCode)
            {
                var json = await responseAreas.Content.ReadAsStringAsync();
                lAreas = JsonSerializer.Deserialize<List<Area>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseResp = await _httpClient.GetAsync("http://localhost:8082/responsable");
            if (responseResp.IsSuccessStatusCode)
            {
                var json = await responseResp.Content.ReadAsStringAsync();
                lResponsables = JsonSerializer.Deserialize<List<Responsable>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseEst = await _httpClient.GetAsync("http://localhost:8082/estado");
            if (responseEst.IsSuccessStatusCode)
            {
                var json = await responseEst.Content.ReadAsStringAsync();
                lEstados = JsonSerializer.Deserialize<List<Estado>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }


        public async Task<IActionResult> OnPost()
        {
            var request = new AuditoriaRequest
            {
                idarea = Area,
                titulo = Titulo,
                fechainicio = FechaInicio,
                fechafin = FechaFin,
                idresponsable = Responsable,
                idestado = Estado
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:8082/auditoria", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index"); // redirige a la lista
            }
            else
            {
                return Page();
            }
        }
    }
}

