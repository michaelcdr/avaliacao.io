using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class CoordenadorController : Controller
    {
        private readonly IUsuarioService _usuarioServico;
        private readonly IUnitOfWork _uow;

        public CoordenadorController(IUsuarioService usuarioServico, IUnitOfWork uow)
        {
            this._usuarioServico = usuarioServico;
            this._uow = uow;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCoordenadorRequest request)
        {
            AppResponse resposta = await _usuarioServico.CriarCoordenador(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AtualizarCoordenadorRequest request)
        {
            AppResponse resposta = await _usuarioServico.AtualizarCoordenador(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Coordenador> coordenadores = await _uow.Usuarios.ObterCoordenadores();

            var coordenadoresDto = coordenadores.Count > 0
                ? coordenadores.Select(coordenador => new CoordenadorDTO(coordenador)).ToList()
                : new List<CoordenadorDTO>();

            return Ok(coordenadoresDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Coordenador coordenador = await _uow.Usuarios.ObterCoordenador(id);

            if (coordenador == null) return NotFound();

            var coordenadorDTO = new CoordenadorDTO(coordenador);

            return Ok(coordenadorDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            Usuario usuario = await _uow.Usuarios.Obter("Coordenador", id);

            if (usuario == null) return NotFound(new AppResponse(false, "Coordenador não encontrado."));

            _uow.Usuarios.Delete(usuario);
            await _uow.CommitAsync();
            return NoContent();
        }
    }
}
