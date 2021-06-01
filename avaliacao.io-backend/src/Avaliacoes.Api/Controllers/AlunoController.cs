using Avaliacoes.Aplicacao.Services;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    public class AlunoController : Controller
    {
        private readonly IUsuarioService _usuarioServico;

        public AlunoController(IUsuarioService usuarioServico)
        {
            this._usuarioServico = usuarioServico;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CriarAlunoRequest request)
        {
            CriarAlunoResponse resposta = await _usuarioServico.CriarAluno(request);

            if (resposta.Sucesso) return Ok(resposta);

            return BadRequest(resposta);
        }
    }
}
