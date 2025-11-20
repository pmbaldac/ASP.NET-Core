using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Front.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Front.Pages.Shared
{
    public class EditarAuditoriaModel : PageModel
    {

        private readonly ILogger<EditarAuditoriaModel> _logger;
        private readonly HttpClient _httpClient;

        public EditarAuditoriaModel(ILogger<EditarAuditoriaModel> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public List<Responsable> lResponsables { get; set; } = new List<Responsable>();
        public List<Area> lAreas { get; set; } = new List<Area>();
        public List<Estado> lEstados { get; set; } = new List<Estado>();

        [BindProperty]
        public Auditoria Auditoria { get; set; }

        public async Task<IActionResult> OnGet(int id, string titulo, int idarea, int idresponsable, 
            DateTime fechainicio, DateTime? fechafin, int idestado)
        {
            await LoadList();

            Auditoria = new Auditoria
            {
                id = id,
                titulo = titulo,
                idarea = idarea,
                idresponsable = idresponsable,
                fechainicio = fechainicio,
                fechafin = fechafin,
                idestado = idestado
            };

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var json = JsonSerializer.Serialize(Auditoria);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("http://localhost:8082/auditoria", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index"); 
            }
            else
            {
                TempData["Message"] = "Error al actualizar auditoría";
                await LoadList();
                return Page();
            }
        }

        private async Task LoadList()
        {
            var responseResponsable = await _httpClient.GetAsync("http://localhost:8082/responsable");
            if (responseResponsable.IsSuccessStatusCode)
            {
                var json = await responseResponsable.Content.ReadAsStringAsync();
                lResponsables = JsonSerializer.Deserialize<List<Responsable>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseArea = await _httpClient.GetAsync("http://localhost:8082/area");
            if (responseArea.IsSuccessStatusCode)
            {
                var json = await responseArea.Content.ReadAsStringAsync();
                lAreas = JsonSerializer.Deserialize<List<Area>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseEstado = await _httpClient.GetAsync("http://localhost:8082/estado");
            if (responseEstado.IsSuccessStatusCode)
            {
                var json = await responseEstado.Content.ReadAsStringAsync();
                lEstados = JsonSerializer.Deserialize<List<Estado>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }

    }
}
