using Avaliacoes.Dominio.DTOs;
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
        private string MSG_HABILIDADE_JAEXISTE = "Já existe uma habilidade com o nome";
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

            if (habilidade == null) return NotFound();

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
                return BadRequest(new { sucesso = false, erros = habilidade.ObterErros() });
            else
            {
                bool existeComMesmoNome = await _uow.Habilidades.Existe(request.CompetenciaId, request.Descritivo, null);

                if (existeComMesmoNome) return BadRequest(new { sucesso = false, erros = new List<string> { MSG_HABILIDADE_JAEXISTE } });

                Competencia competencia = await _uow.Compentencias.Get(request.CompetenciaId);

                if (competencia == null) return BadRequest(new { sucesso = false, erros = new List<string> { MSG_COMPETENCIA_NAOEXISTE } });

                _uow.Habilidades.Add(habilidade);
                await _uow.CommitAsync();

                var uri = Url.Action("Get", new { id = habilidade.Id });

                return Created(uri, habilidade.ToDTO());
            }
        }

       







        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] CompetenciaRequest request)
        //{
        //    Competencia competencia = await _uow.Compentencias.Get(id);

        //    if (competencia == null) return NotFound();

        //    if (!competencia.TaValido())
        //        return BadRequest(new { sucesso = false, erros = competencia.ObterErros() });
        //    else
        //    {
        //        bool existeComMesmoNome = await _uow.Compentencias.Existe(request.DisciplinaId, request.Descritivo, request.Id);

        //        if (existeComMesmoNome) return BadRequest(new { sucesso = false, erros = new List<string> { MSG_COMPENTENCIA_JAEXISTE } });

        //        Disciplina disciplina = await _uow.Disciplinas.Get(request.DisciplinaId);

        //        if (disciplina == null) return BadRequest(new { sucesso = false, erros = new List<string> { MSG_DISCIPLINA_NAOEXISTE } });

        //        competencia.Atualizar(request);
        //        await _uow.CommitAsync();

        //        return Ok(new { sucesso = true, mensagem = "Competencia atualizada com sucesso." });
        //    }
        //}

     
    }
}
