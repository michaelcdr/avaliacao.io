using Avaliacoes.Api.Configuration;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AutenticacaoController(
            IUnitOfWork uow, SignInManager<Usuario> signInManager, UserManager<Usuario> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            this._uow = uow;
            this._signInManager = signInManager;
            this._userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        /// <summary>
        /// Gera um token para usar na autenticação da API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioEncontrado = await _userManager.FindByNameAsync(model.Login);
                if (usuarioEncontrado == null) return BadRequest(new AppResponse(false, "Usuário não encontrado."));

                var result = await _signInManager.CheckPasswordSignInAsync(usuarioEncontrado,   model.Password, false);

                if (!result.Succeeded)
                    return BadRequest(new AppResponse(false, "Não foi possível gerar o token."));
                else
                {
                    string tokenString = await CriarToken(model, usuarioEncontrado);
                    return Ok(tokenString);
                }
            }
            return BadRequest(new AppResponse(false, "Não foi possível gerar o token."));
        }

        private async Task<string> CriarToken(LoginModel model, Usuario usuarioEncontrado)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var rolesDoUsuario = await _userManager.GetRolesAsync(usuarioEncontrado);

            var role = rolesDoUsuario.First();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", usuarioEncontrado.Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }

        /// <summary>
        /// Desloga um usuário autenticado.
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public async Task Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
        }
    }
}
