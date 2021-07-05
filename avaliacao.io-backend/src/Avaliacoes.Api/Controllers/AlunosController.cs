using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Cadastra um novo aluno, esse processo deve ser feito por um professor ou coordenador.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Post([FromBody] CriarAlunoRequest request)
        {
            AppResponse resposta = await _usuarioServico.CriarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        /// <summary>
        /// Atualiza um aluno especifico, esse processo deve ser feito por um professor ou coordenador.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Put([FromBody] AtualizarAlunoRequest request)
        {
            AppResponse resposta = await _usuarioServico.AtualizarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        /// <summary>
        /// Obtem dados de um aluno.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            List<Aluno> alunos = await _uow.Usuarios.ObterAlunos();

            var alunosDto = alunos.Count > 0
                ? alunos.Select(aluno => new AlunoComDisciplinaDTO(aluno)).ToList()
                : new List<AlunoComDisciplinaDTO>();

            return Ok(alunosDto);
        }

        /// <summary>
        /// Obtem todos alunos de uma disciplina especifica.
        /// </summary>
        /// <param name="idDisciplina"></param>
        /// <returns></returns>
        [HttpGet("ObterTodosPorDisciplina/{idDisciplina}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ObterTodosPorDisciplina(int idDisciplina)
        {
            List<Aluno> alunos = await _uow.Usuarios.ObterAlunosPorDisciplina(idDisciplina);

            var alunosDto = alunos.Count > 0
                ? alunos.Select(aluno => new AlunoComDisciplinaDTO(aluno)).ToList()
                : new List<AlunoComDisciplinaDTO>();

            return Ok(alunosDto);
        }

        /// <summary>
        /// Adicionar aluno em disciplina, esse processo deve ser feito por um professor ou coordenador.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("AdicionarEmDisciplinas")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> AdicionarEmDisciplinas([FromBody] VincularAlunoDisciplinasRequest request)
        {
            AppResponse resposta = await _usuarioServico.VincularDisciplinasEmAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        /// <summary>
        /// Remove um aluno, esse processo deve ser feito por um professor ou coordenador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Delete(string id)
        {
            Usuario usuario = await _uow.Usuarios.Obter("Aluno", id);

            if (usuario == null) return NotFound(new AppResponse(false, "Aluno não encontrado."));

            _uow.Usuarios.Delete(usuario);
            await _uow.CommitAsync();
            return NoContent();
        }

        /// <summary>
        /// Endpoint responsável por avaliar a dimensão de uma habilidade de um aluno, a nota deve ser de 0 a 2, 
        /// quando ja existir uma avaliação registrada para o mesmo semestre e aluno a mesma será atualizada, esse processo deve ser feito por um professor.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Avaliar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor")]
        public async Task<IActionResult> Avaliar([FromBody] AvaliarAluno request)
        {
            AppResponse resposta = await _usuarioServico.AvaliarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        /// <summary>
        /// Obtem dados referente a grade curricular do aluno.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("ObterGradeCurricular/{usuarioId}")]
        public async Task<IActionResult> ObterGradeCurricular(string usuarioId)
        {
            AppResponse resposta = await _usuarioServico.ObterGradeCurricular(usuarioId, User.Identity.Name);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        /// <summary>
        /// Obtem notas de um aluno, referente a cada habilidade e dimensão encontrada.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("ObterNotas/{usuarioId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ObterNotas(string usuarioId)
        {
            AppResponse resposta = await _usuarioServico.ObterNotas(usuarioId, User.Identity.Name);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor")]
        [HttpPost("Importar")]
        public async Task<IActionResult> Importar([FromForm] ImportarAlunos importarAlunos)
        {
            AppResponse resposta = await _usuarioServico.ImportarAlunos(importarAlunos);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }
    }
}
