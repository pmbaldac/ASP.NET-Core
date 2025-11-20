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

namespace Front.Pages
{
    public class HallazgoModel : PageModel
    {
        private readonly ILogger<HallazgoModel> _logger;
        private readonly HttpClient _httpClient;

        public HallazgoModel(ILogger<HallazgoModel> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public List<Hallazgo> lHallazgos { get; set; } = new List<Hallazgo>();
        public List<Auditoria> lAuditorias { get; set; } = new List<Auditoria>();
        public List<Tipo> lTipo { get; set; } = new List<Tipo>();
        public List<Severidad> lSeveridad { get; set; } = new List<Severidad>();

        [BindProperty(SupportsGet = true)]
        public string AuditoriaFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TipoFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SeveridadFiltro { get; set; }

        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("http://localhost:8082/hallazgo");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<Hallazgo>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!string.IsNullOrEmpty(AuditoriaFiltro))
                    data = data.Where(a => a.titulo == AuditoriaFiltro).ToList();

                if (!string.IsNullOrEmpty(TipoFiltro))
                    data = data.Where(a => a.tipo == TipoFiltro).ToList();

                if (!string.IsNullOrEmpty(SeveridadFiltro))
                    data = data.Where(a => a.severidad == SeveridadFiltro).ToList();

                lHallazgos = data;
            }

            var responseAuditoria = await _httpClient.GetAsync("http://localhost:8082/auditoria");
            if (responseAuditoria.IsSuccessStatusCode)
            {
                var json = await responseAuditoria.Content.ReadAsStringAsync();
                lAuditorias = JsonSerializer.Deserialize<List<Auditoria>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseTipo = await _httpClient.GetAsync("http://localhost:8082/tipo");
            if (responseTipo.IsSuccessStatusCode)
            {
                var json = await responseTipo.Content.ReadAsStringAsync();
                lTipo = JsonSerializer.Deserialize<List<Tipo>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            var responseSeveridad = await _httpClient.GetAsync("http://localhost:8082/severidad");
            if (responseSeveridad.IsSuccessStatusCode)
            {
                var json = await responseSeveridad.Content.ReadAsStringAsync();
                lSeveridad = JsonSerializer.Deserialize<List<Severidad>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:8082/hallazgo/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = $"Hallazgo {id} eliminado correctamente";
            }
            else
            {
                TempData["Message"] = $"Error al eliminar hallazgo {id}";
            }

            return RedirectToPage();
        }
    }
}
