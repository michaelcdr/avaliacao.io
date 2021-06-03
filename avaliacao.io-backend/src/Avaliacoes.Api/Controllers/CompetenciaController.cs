using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetenciaController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public CompetenciaController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet("ObterTodasPorDisciplina/{idDisciplina}")]
        public async Task<IActionResult> ObterTodasPorDisciplina(int idDisciplina)
        {
            IList<Competencia> competencias = await _uow.Compentencias.ObterTodasPorDisciplina(idDisciplina);

            List<CompenciaDTO> disciplinasDTOs = competencias.Select(e => e.ToDTO()).ToList();

            return Ok(disciplinasDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Competencia> competencias = await _uow.Compentencias.ObterTodos();

            List<CompenciaDTO> competenciasDTOs = competencias.Select(e => e.ToDTO()).ToList();

            return Ok(competenciasDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Competencia competencia = await _uow.Compentencias.Get(id);

            if (competencia == null) return NotFound();

            return Ok(competencia.ToDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompetenciaRequest request)
        {
            var competencia = new Competencia(request.DisciplinaId, request.Nome, request.Descritivo);

            if (!competencia.TaValido())
                return BadRequest(new { sucesso = false, erros = competencia.ObterErros() });
            else
            {
                if (await _uow.Compentencias.Existe(request.DisciplinaId, request.Nome))
                    return BadRequest(new { sucesso = false, erros = new List<string> { "Já existe uma disciplina com o nome" } });

                _uow.Compentencias.Add(competencia);
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = competencia.Id });

                return Created(uri, competencia.ToDTO());
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CompetenciaRequest request)
        {
            Competencia competencia = await _uow.Compentencias.Get(id);

            if (competencia == null) return NotFound();

            if (!competencia.TaValido())
                return BadRequest(new { sucesso = false, erros = competencia.ObterErros() });
            else
            {
                competencia.Nome = request.Nome;
                competencia.Descritivo = request.Descritivo;
                competencia.DisciplinaId = request.DisciplinaId;
                await _uow.CommitAsync();

                return Ok(new { sucesso = true });
            }
        }

        [HttpDelete("{id}")]
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
