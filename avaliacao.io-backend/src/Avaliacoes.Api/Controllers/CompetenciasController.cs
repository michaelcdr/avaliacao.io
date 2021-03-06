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
    [ApiController]
    public class CompetenciasController : ControllerBase
    {
        private const string MSG_DISCIPLINA_NAOEXISTE = "A Disciplina informada não existe.";
        private const string MSG_DISCIPLINA_CRIADA = "Competência criada com sucesso.";
        private string MSG_COMPENTENCIA_JAEXISTE = "Já existe uma disciplina com o nome informado.";
        private string MSG_ERRO = "Ops, algo deu errado.";
        private readonly IUnitOfWork _uow;

        public CompetenciasController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet("ObterTodasPorDisciplina/{idDisciplina}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ObterTodasPorDisciplina(int idDisciplina)
        {
            IList<Competencia> competencias = await _uow.Compentencias.ObterTodasPorDisciplina(idDisciplina);

            List<CompenciaDTO> disciplinasDTOs = competencias.Select(e => e.ToDTO()).ToList();

            return Ok(disciplinasDTOs);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            IList<Competencia> competencias = await _uow.Compentencias.ObterTodos();

            List<CompenciaDTO> competenciasDTOs = competencias.Select(e => e.ToDTO()).ToList();

            return Ok(competenciasDTOs);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Get(int id)
        {
            Competencia competencia = await _uow.Compentencias.Get(id);

            if (competencia == null) return NotFound();

            return Ok(competencia.ToDTO());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<ActionResult> Post([FromBody] CompetenciaRequest request)
        {
            var competencia = new Competencia(request.DisciplinaId, request.Nome, request.Descritivo);

            if (!competencia.TaValido())
                return BadRequest(new { sucesso = false, erros = competencia.ObterErros() });
            else
            {
                bool existeComMesmoNome = await _uow.Compentencias.Existe(request.DisciplinaId, request.Nome, null);
                
                if (existeComMesmoNome) return BadRequest(new AppResponse(MSG_ERRO, false, new List<string> { MSG_COMPENTENCIA_JAEXISTE }));

                Disciplina disciplina = await _uow.Disciplinas.Get(request.DisciplinaId);

                if (disciplina == null) return BadRequest(new AppResponse(MSG_ERRO, false, new List<string> { MSG_DISCIPLINA_NAOEXISTE }));

                _uow.Compentencias.Add(competencia);
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = competencia.Id });

                return Created(uri, new AppResponse(true, MSG_DISCIPLINA_CRIADA, competencia.ToDTO()));
            }
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Put(int id, [FromBody] CompetenciaRequest request)
        {
            Competencia competencia = await _uow.Compentencias.Get(id);

            if (competencia == null) return NotFound(new AppResponse(true, "A competencia informada não existe."));

            competencia.Atualizar(request);

            if (!competencia.TaValido())
                return BadRequest(new AppResponse (false, "Ocorreram erros de validação.", competencia.ObterErros()));
            else
            {
                bool existeComMesmoNome = await _uow.Compentencias.Existe(request.DisciplinaId, request.Nome, id);

                if (existeComMesmoNome) return BadRequest(new AppResponse(MSG_ERRO, false, new List<string> { MSG_COMPENTENCIA_JAEXISTE } ));
                
                Disciplina disciplina = await _uow.Disciplinas.Get(request.DisciplinaId);
                
                if (disciplina == null) return BadRequest(new AppResponse(MSG_ERRO, false, new List<string> { MSG_DISCIPLINA_NAOEXISTE } ));

                await _uow.CommitAsync();

                return Ok(new AppResponse(true, "Competência atualizada com sucesso.",competencia.ToDTO()));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Delete(int id)
        {
            Competencia competencia = await _uow.Compentencias.Get(id);

            if (competencia == null) return NotFound();

            _uow.Compentencias.Delete(competencia);
            await _uow.CommitAsync();

            return NoContent();
        }
    }
}
