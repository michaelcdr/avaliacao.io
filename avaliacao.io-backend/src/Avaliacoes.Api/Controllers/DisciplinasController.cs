using Avaliacoes.Dominio.DTOs;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Disciplina> disciplinas = await _uow.Disciplinas.ObterTodasComProfessores();

            List<DisciplinaDTO> disciplinasDTOs = disciplinas.Select(e => e.ToDTO()).ToList();

            return Ok(disciplinasDTOs);
        }

        /// <summary>
        /// Metódo responsável por obter uma disciplina pelo Id.
        /// </summary>
        /// <param name="idDisciplina">Id da disciplina</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{idDisciplina}")]
        public async Task<ActionResult> Get(int idDisciplina)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(idDisciplina);
            
            if (disciplina == null) return NotFound();

            return Ok(disciplina.ToDTO());
        }

        /// <summary>
        /// Método responsável por criar uma nova disciplina.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<ActionResult> Post([FromBody] DisciplinaRequest request)
        {
            var disciplina = new Disciplina(request.Nome, request.Descritivo, request.Horario);

            if (!disciplina.TaValido())
                return BadRequest(new { erros = disciplina.ObterErros() });
            else
            {
                _uow.Disciplinas.Add(disciplina);
                await _uow.CommitAsync();

                foreach (string professorId in request.Professores)
                {
                    Professor professor = await _uow.Usuarios.ObterProfessor(professorId);
                    disciplina.AdicionarProfessor(professor);
                }
                
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = disciplina.Id });

                return Created(uri, new { Id = disciplina.Id, Nome = disciplina.Nome });
            }
        }

        /// <summary>
        /// Método responsável por atualizar uma disciplina.
        /// </summary>
        /// <param name="idDisciplina">Id da disciplina</param>
        /// <param name="disciplinaDTO">Objeto com dados da disciplina.</param>
        /// <returns></returns>
        [HttpPut("{idDisciplina}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Put(int idDisciplina, [FromBody] DisciplinaRequest disciplinaDTO)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(idDisciplina);

            if (disciplina == null) return NotFound();

            disciplina.Atualizar(disciplinaDTO);

            if (!disciplina.TaValido())
                return BadRequest(new { erros = disciplina.ObterErros() });
            else
            {
                List<Professor> professorsInformados = await _uow.Usuarios.ObterProfessores(disciplinaDTO.Professores);
                disciplina.AdicionarProfessores(professorsInformados);
                
                await _uow.CommitAsync();

                return Ok();
            }   
        }

        /// <summary>
        /// Método responsável por remover uma disciplina, atenção todos dados vinculados a disciplina consequentemente serão removidos.
        /// </summary>
        /// <param name="idDisciplina">Id da disciplina.</param>
        /// <returns></returns>
        [HttpDelete("{idDisciplina}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Delete(int idDisciplina)
        {
            Disciplina disciplina = await _uow.Disciplinas.ObterComProfessores(idDisciplina);

            if (disciplina == null) return NotFound();

            _uow.Disciplinas.Delete(disciplina);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}