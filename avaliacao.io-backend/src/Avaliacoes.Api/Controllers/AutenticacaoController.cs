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

        [HttpPost]
        public async Task<IActionResult> Token(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, true);

                if (!result.Succeeded)
                    return Unauthorized();
                else
                {
                    // criando token (header + payload >> direitos + signature)...

                    var direitos = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.Login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("api-avaliacoes-security-key"));
                    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "Avaliacoes.Api",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credenciais,
                        expires: DateTime.Now.AddMinutes(30)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
            }
            return BadRequest();
        }        
    }
}
