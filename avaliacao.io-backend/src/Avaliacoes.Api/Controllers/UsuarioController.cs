using Avaliacoes.Dominio.Entidades;
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
    public class UsuarioController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoUsuario> _tipoUsuarioManager;

        public UsuarioController(
            IUnitOfWork uow, 
            SignInManager<Usuario> signInManager,
            UserManager<Usuario> userManager,
            RoleManager<TipoUsuario> tipoUsuarioManager)
        {
            this._uow = uow;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._tipoUsuarioManager = tipoUsuarioManager;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(string login, string senha)
        {
            var usuario = new Usuario() 
            {
                Nome = login,
                Email = "michaelcdr@hotmail.com",               
            };
            IdentityResult resultado = await _userManager.CreateAsync(usuario, senha);

            if (!resultado.Succeeded) return BadRequest();
       
            IdentityResult resultadoRole = await _userManager.AddToRoleAsync(usuario, "coordenador");
            if (!resultadoRole.Succeeded)  return BadRequest();

            return View();
        }
    }
}
