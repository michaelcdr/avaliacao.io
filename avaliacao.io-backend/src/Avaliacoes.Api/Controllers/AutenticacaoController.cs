﻿using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<Usuario> _signInManager;

        public AutenticacaoController(IUnitOfWork uow, SignInManager<Usuario> signInManager)
        {
            this._uow = uow;
            this._signInManager = signInManager;
        }

        /// <summary>
        /// Gera um token para usar na autenticação da API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Autenticar")]
        public async Task<IActionResult> GerarToken(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, true);

                if (!result.Succeeded)
                    return BadRequest(new AppResponse(false, "Não foi possível gerar o token."));
                else
                {
                    // criando token (header + payload >> direitos + signature)...

                    var direitos = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var credenciais = new SigningCredentials(
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("api-avaliacoes-security-key")), 
                        SecurityAlgorithms.HmacSha256
                    );

                    var token = new JwtSecurityToken(
                        issuer: "Avaliacoes.Api",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credenciais,
                        expires: DateTime.Now.AddMinutes(30)
                    );

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenString = tokenHandler.WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest(new AppResponse(false, "Não foi possível gerar o token."));
        }

        [HttpPost("Logout")]
        public async Task Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
        }
    }
}
