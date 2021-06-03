using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.InputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
        private readonly IUsuarioService _usuarioServico;

        public AlunoController(IUsuarioService usuarioServico)
        {
            this._usuarioServico = usuarioServico;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(CriarAlunoRequest request)
        {
            AppResponse resposta = await _usuarioServico.CriarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosPorDisciplina(int idDisciplina)
        {
            return Ok();
        }
    }
}
