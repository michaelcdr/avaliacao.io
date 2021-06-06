using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlunosController : Controller
    {
        private readonly IUsuarioService _usuarioServico;
        private readonly IUnitOfWork _uow;
        public AlunosController(IUsuarioService usuarioServico, IUnitOfWork uow)
        {
            this._usuarioServico = usuarioServico;
            this._uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarAlunoRequest request)
        {
            AppResponse resposta = await _usuarioServico.CriarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Aluno> alunos = await _uow.Usuarios.ObterAlunos();

            var alunosDto = alunos.Count > 0
                ? alunos.Select(aluno => new AlunoComDisciplinaDTO(aluno)).ToList()
                : new List<AlunoComDisciplinaDTO>();

            return Ok(alunosDto);
        }

        [HttpGet("ObterTodosPorDisciplina/{idDisciplina}")]
        public async Task<IActionResult> ObterTodosPorDisciplina(int idDisciplina)
        {
            List<Aluno> alunos = await _uow.Usuarios.ObterAlunosPorDisciplina(idDisciplina);

            var alunosDto = alunos.Count > 0
                ? alunos.Select(aluno => new AlunoComDisciplinaDTO(aluno)).ToList()
                : new List<AlunoComDisciplinaDTO>();

            return Ok(alunosDto);
        }

        [HttpPost("AdicionarEmDisciplinas")]
        public async Task<IActionResult> AdicionarEmDisciplinas([FromBody] VincularAlunoDisciplinasRequest request)
        {
            AppResponse resposta = await _usuarioServico.VincularDisciplinasEmAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }
    }
}
