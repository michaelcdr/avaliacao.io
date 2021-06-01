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

        [HttpGet]
        public async Task<IActionResult> ObterTodasPorDisciplina(int idDisciplina)
        {
            IList<Competencia> competencias = await _uow.Compentencias.ObterTodasPorDisciplina(idDisciplina);

            List<CompenciaDTO> disciplinasDTOs = competencias.Select(e => e.ToDTO()).ToList();

            return Ok(disciplinasDTOs);
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
                return BadRequest(new { erros = competencia.ObterErros() });
            else
            {
                _uow.Compentencias.Add(competencia);
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = competencia.Id });

                return Created(uri, new { Id = competencia.Id, Nome = competencia.Nome });
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] DisciplinaRequest disciplinaDTO)
        //{
        //    Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(id);

        //    if (disciplina == null) return NotFound();

        //    if (ModelState.IsValid)
        //    {
        //        disciplina.Descritivo = disciplinaDTO.Descritivo;
        //        disciplina.Nome = disciplinaDTO.Nome;
        //        disciplina.Horario = disciplinaDTO.Horario;
        //        disciplina.Professores = new List<Professor>();

        //        List<Professor> professorsInformados = await _uow.Usuarios.ObterProfessores(disciplinaDTO.Professores);
        //        disciplina.AdicionarProfessores(professorsInformados);

        //        await _uow.CommitAsync();

        //        return Ok();
        //    }
        //    else
        //        return BadRequest(new { erros = new List<string> { "Ocorreram erros de validação." } });
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(id);

        //    if (disciplina == null) return NotFound();

        //    _uow.Disciplinas.Delete(disciplina);
        //    await _uow.CommitAsync();
        //    return NoContent();
        //}
    }
}
