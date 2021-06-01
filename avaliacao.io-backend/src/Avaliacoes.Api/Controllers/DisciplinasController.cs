using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Metódo responsável por retornar disciplinas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Disciplina> disciplinas = await _uow.Disciplinas.ObterTodasComProfessores();

            List<DisciplinaDTO> disciplinasDTOs = disciplinas.Select(e => e.ToDTO()).ToList();

            return Ok(disciplinasDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(id);
            
            if (disciplina == null) return NotFound();

            return Ok(disciplina.ToDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DisciplinaRequest request)
        {
            var disciplina = new Disciplina(request.Nome, request.Descritivo);

            if (ModelState.IsValid)
            {
                _uow.Disciplinas.Add(disciplina);
                await _uow.CommitAsync();

                foreach (var professorId in request.Professores)
                {
                    Professor professor = await _uow.Usuarios.ObterProfessor(professorId);
                    disciplina.AdicionarProfessor(professor);
                }
                
                await _uow.CommitAsync();
                var uri = Url.Action("Get", new { id = disciplina.Id });

                return Created(uri, new { Id = disciplina.Id, Nome = disciplina.Nome });
            }
            else
                return BadRequest(new { erros = new List<string> { "Ocorreram erros de validação." } });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DisciplinaRequest disciplinaDTO)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(id);

            if (disciplina == null) return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(new { erros = disciplina.ObterErros() });
            else
            {
                disciplina.Descritivo = disciplinaDTO.Descritivo;
                disciplina.Nome = disciplinaDTO.Nome;
                disciplina.Horario = disciplinaDTO.Horario;
                disciplina.Professores = new List<Professor>();

                List<Professor> professorsInformados = await _uow.Usuarios.ObterProfessores(disciplinaDTO.Professores);
                disciplina.AdicionarProfessores(professorsInformados);
                
                await _uow.CommitAsync();

                return Ok();
            }   
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(id);

            if (disciplina == null) return NotFound();

            _uow.Disciplinas.Delete(disciplina);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}