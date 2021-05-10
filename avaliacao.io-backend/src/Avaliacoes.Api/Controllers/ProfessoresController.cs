using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProfessoresController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Usuario> usuarios = await _uow.Usuarios.ObterProfessores();
            var professores = new List<ProfessorComDisciplinaDTO>();
            if (usuarios.Count > 0)
            {
                professores = usuarios.Select(u => new ProfessorComDisciplinaDTO
                {
                    Id = u.Id,
                    Email = u.Email,
                    Nome = u.Nome
                }).ToList();
            }
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Professor professor = await _uow.Usuarios.ObterProfessor(id);

            if (professor == null) return NotFound();

            var professorComDisciplinaDTO = new ProfessorComDisciplinaDTO(professor.Usuario);
            return Ok(professorComDisciplinaDTO);
        }

        [HttpPost]
        public async Task Post([FromBody] ProfessorDTO professorDTO)
        {
            var usuario = new Usuario
            {
                Nome = professorDTO.Nome,
                Senha = professorDTO.Senha,
                Email = professorDTO.Email,
                Tipo = "Professor",
                Professor = new Professor 
                { 
                    Disciplinas = new List<Disciplina>() 
                    {
                    
                    } 
                }
            };

            _uow.Usuarios.Add(usuario);
            await _uow.CommitAsync();

            //usuario.Professor = new Professor() { UsuarioId = usuario.Id, Usuario = usuario };
            //await _uow.CommitAsync();
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
