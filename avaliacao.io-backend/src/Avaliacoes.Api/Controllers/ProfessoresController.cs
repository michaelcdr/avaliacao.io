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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsuarioService _usuarioService;
        public ProfessoresController(IUnitOfWork uow, IUsuarioService usuarioService)
        {
            this._uow = uow;
            this._usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            List<Usuario> usuarios = await _uow.Usuarios.ObterProfessores();

            var professores = usuarios.Count > 0
                ? usuarios.Select(usuario => new ProfessorComDisciplinaDTO(usuario)).ToList()
                : new List<ProfessorComDisciplinaDTO>();

            return Ok(professores);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(string id)
        {
            Professor professor = await _uow.Usuarios.ObterProfessor(id);

            if (professor == null) return NotFound();

            var professorComDisciplinaDTO = new ProfessorComDisciplinaDTO(professor.Usuario);

            return Ok(professorComDisciplinaDTO);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Post([FromBody] CriarProfessorRequest request)
        {
            AppResponse response = await _usuarioService.CriarProfessor(request);
            
            if (response.Sucesso) return Ok(response);

            return BadRequest(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Put([FromBody] AtualizarProfessorRequest request)
        {
            AppResponse response = await _usuarioService.AtualizarProfessor(request);

            if (response.Sucesso) return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Professor, Coordenador")]
        public async Task<IActionResult> Delete(string id)
        {
            Usuario usuario = await _uow.Usuarios.Obter("Professor", id);

            if (usuario == null) return NotFound(new AppResponse(false, "Professor não encontrado."));

            _uow.Usuarios.Delete(usuario);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}