using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilidadesController : ControllerBase
    {
        private const string MSG_COMPETENCIA_NAOEXISTE = "A Competencia informada não existe.";
        private readonly IUnitOfWork _uow;
        private string MSG_HABILIDADE_JAEXISTE = "Já existe uma habilidade com o nome informado.";
        private string MSG_ERRO = "Ops, algo deu errado.";
        public HabilidadesController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Habilidade> habilidades = await _uow.Habilidades.ObterTodasComDimensoes();

            List<HabilidadeDTO> habilidadesDTOs = habilidades.Select(e => e.ToDTO()).ToList();

            return Ok(habilidadesDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            Habilidade habilidade = await _uow.Habilidades.ObterComDimensoes(id);

            if (habilidade == null) return NotFound(new AppResponse(false, "Ops, algo deu errado.",new List<string> { "Nenhuma habilidade encontrada." }));

            return Ok(habilidade.ToDTO());
        }

        [HttpGet("ObterTodasPorCompetencia/{idCompetencia}")]
        public async Task<IActionResult> ObterTodasPorCompetencia(int idCompetencia)
        {
            IList<Habilidade> habilidades = await _uow.Habilidades.ObterTodasPorCompetencia(idCompetencia);

            List<HabilidadeDTO> habilidadesDTOs = habilidades.Select(e => e.ToDTO()).ToList();

            return Ok(habilidadesDTOs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Habilidade habilidade = await _uow.Habilidades.Get(id);

            if (habilidade == null) return NotFound();

            _uow.Habilidades.Delete(habilidade);
            await _uow.CommitAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] HabilidadeRequest request)
        {
            var dimensoes = request.Dimensoes != null
                ? request.Dimensoes.Select(e => new Dimensao { Codigo = e.Codigo, Nome = e.Nome }).ToList()
                : new List<Dimensao>();

            var habilidade = new Habilidade(request.CompetenciaId, request.Nome, request.Descritivo, dimensoes);

            if (!habilidade.TaValido())
                return BadRequest(new AppResponse(false, MSG_ERRO, habilidade.ObterErros()));
            else
            {
                bool existeComMesmoNome = await _uow.Habilidades.Existe(request.CompetenciaId, request.Nome, null);

                if (existeComMesmoNome) return BadRequest(new AppResponse(false, MSG_ERRO, new List<string> { MSG_HABILIDADE_JAEXISTE }));

                Competencia competencia = await _uow.Compentencias.Get(request.CompetenciaId);

                if (competencia == null) return BadRequest(new AppResponse(false, MSG_ERRO, new List<string> { MSG_COMPETENCIA_NAOEXISTE }));

                _uow.Habilidades.Add(habilidade);
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = habilidade.Id });

                return Created(uri, new AppResponse(true,"Habilidade criada com sucesso.", habilidade.ToDTO()));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HabilidadeRequest request)
        {
            Habilidade habilidade = await _uow.Habilidades.ObterComDimensoes(id);

            if (habilidade == null) return NotFound();

            habilidade.Atualizar(request);

            if (!habilidade.TaValido())
                return BadRequest(new AppResponse(false, MSG_ERRO, habilidade.ObterErros()));
            else
            {
                bool existeComMesmoNome = await _uow.Compentencias.Existe(request.CompetenciaId, request.Descritivo, id);

                if (existeComMesmoNome) return BadRequest(new AppResponse(false, MSG_HABILIDADE_JAEXISTE, new List<string> { MSG_HABILIDADE_JAEXISTE }));

                Competencia competencia = await _uow.Compentencias.Get(request.CompetenciaId);

                if (competencia == null) return BadRequest(new AppResponse(false, MSG_ERRO, new List<string> { MSG_COMPETENCIA_NAOEXISTE }));

                await _uow.CommitAsync();

                return Ok(new AppResponse(true, "Competencia atualizada com sucesso."));
            }
        }
    }
}
