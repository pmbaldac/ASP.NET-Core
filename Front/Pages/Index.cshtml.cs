using Front.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace Front.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        public List<Auditoria> lAuditorias { get; set; } = new List<Auditoria>();
        public List<Responsable> lResponsables { get; set; } = new List<Responsable>();
        public List<Area> lAreas { get; set; } = new List<Area>();
        public List<Estado> lEstados { get; set; } = new List<Estado>();

        [BindProperty(SupportsGet = true)]
        public string ResponsableFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AreaFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public string EstadoFiltro { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FechaInicio { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FechaFin { get; set; }

        public async Task OnGet()
        {
            var response = await _httpClient.GetAsync("http://localhost:8082/auditoria");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<Auditoria>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (!string.IsNullOrEmpty(ResponsableFiltro))
                    data = data.Where(a => a.responsable == ResponsableFiltro).ToList();

                if (!string.IsNullOrEmpty(AreaFiltro))
                    data = data.Where(a => a.area == AreaFiltro).ToList();

                if (!string.IsNullOrEmpty(EstadoFiltro))
                    data = data.Where(a => a.estado == EstadoFiltro).ToList();
                               
                if (FechaInicio.HasValue)
                    data = data.Where(a => a.fechainicio.Date >= FechaInicio.Value.Date).ToList();

                if (FechaFin.HasValue)
                {
                    data = data
                    .Where(a => a.fechafin.HasValue && a.fechafin.Value.Date <= FechaFin.Value.Date)
                    .ToList();
                }

                lAuditorias = data;
            }

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

