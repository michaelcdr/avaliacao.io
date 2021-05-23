using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
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
        public async Task<IActionResult> Get()
        {
            List<Usuario> usuarios = await _uow.Usuarios.ObterProfessores();
            var professores = new List<ProfessorComDisciplinaDTO>();
            
            if (usuarios.Count > 0)
                professores = usuarios.Select(usuario => new ProfessorComDisciplinaDTO(usuario)).ToList();
            
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Professor professor = await _uow.Usuarios.ObterProfessor(id);

            if (professor == null) return NotFound();

            var professorComDisciplinaDTO = new ProfessorComDisciplinaDTO(professor.Usuario);

            return Ok(professorComDisciplinaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarProfessorRequest request)
        {
            CriarProfessorResponse response = await _usuarioService.CriarProfessor(request);
            
            if (response.Sucesso) return Ok(response);

            return BadRequest(response);
        }
    }
}
