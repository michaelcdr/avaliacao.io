using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinasController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public DisciplinasController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var disciplinas = await _uow.Disciplinas.GetAll();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Disciplina disciplina = await _uow.Disciplinas.Get(id);
            
            if (disciplina == null) return NotFound();

            return Ok(disciplina);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaDTO disciplinaDto)
        {
            var disciplina = new Disciplina(disciplinaDto.Nome, disciplinaDto.Descritivo);

            if (ModelState.IsValid)
            {
                _uow.Disciplinas.Add(disciplina);
                await _uow.CommitAsync();

                //var professoresInformados = _uow.Usuarios.GetAll();
                //disciplina.VincularProfessores(disciplinaDto.Professores);
                //await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = disciplina.Id });
                return Created(uri, disciplina);
            }
            else
                return BadRequest(new { erros = new List<string> { "Ocorreram erros de validação." } });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DisciplinaDTO disciplinaDTO)
        {
            Disciplina disciplina = await _uow.Disciplinas.Get(id);

            if (disciplina == null) return NotFound();

            if (ModelState.IsValid)
            {
                disciplina.Descritivo = disciplinaDTO.Descritivo;
                disciplina.Nome = disciplinaDTO.Nome;
                await _uow.CommitAsync();

                //var professoresInformados = _uow.Usuarios.GetAll();
                //disciplina.VincularProfessores(disciplinaDto.Professores);
                //await _uow.CommitAsync();

                return Ok();
            }
            else
                return BadRequest(new { erros = new List<string> { "Ocorreram erros de validação." }  });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Disciplina disciplina = await _uow.Disciplinas.Get(id);

            if (disciplina == null) return NotFound();

            _uow.Disciplinas.Delete(disciplina);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}
