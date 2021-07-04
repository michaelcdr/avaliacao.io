using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Avaliacoes.Api.DotNetCore3.Controllers
{
    public class DisciplinaController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> GerarToken(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, true, true);
 
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
            return BadRequest(new AppResponse(false, "Não foi possível gerar o token."));
        }
    }
}
