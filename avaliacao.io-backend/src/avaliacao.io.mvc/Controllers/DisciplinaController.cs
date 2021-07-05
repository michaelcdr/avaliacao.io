using Avaliacoes.Aplicacao.Clients;
using Avaliacoes.Dominio.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace avaliacao.io.mvc.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly ILogger<DisciplinaController> _logger;
        private readonly DisciplinaApiClient _api;

        public DisciplinaController(ILogger<DisciplinaController> logger, DisciplinaApiClient api)
        {
            _logger = logger;
            _api = api;
        }
        
        public IActionResult Importar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Importar(IFormFile Arquivo)
        {
            try
            {
                if (Arquivo.Length == 0)
                    ModelState.AddModelError("Arquivo", "Selecione o arquivo.");
                else
                {
                    await _api.Importar(new ImportarDisciplinas() { Arquivo = Arquivo });
                    ViewBag.Retorno = "Importação finalizada com sucesso.";
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Arquivo", "Não foi possivel uplodear o arquivo.");
            }
            return View();
        }
    }
}
