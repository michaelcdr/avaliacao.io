using Avaliacoes.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public class SeedService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoUsuario> _roleManager;

        public SeedService(
            UserManager<Usuario> userManager,
            RoleManager<TipoUsuario> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            await CriaTipoDeUsuarioAlunoSeNaoExiste();
            await CriarTipoDeUsuarioCoordenadorSeNaoExiste();
            await CriaTipoDeUsuarioProfessorSeNaoExiste();
            await CriarUsuarios();
        }

        private async Task CriarUsuarios()
        {
            IList<Usuario> coordenadores = await _userManager.GetUsersInRoleAsync("Coordenador");
            IList<Usuario> professores = await _userManager.GetUsersInRoleAsync("Professor");
            IList<Usuario> alunos = await _userManager.GetUsersInRoleAsync("Aluno");

            if (!coordenadores.Any())
            {
                var usuario = new Usuario("Michael","michael", "michaelcdr@hotmail.com");

                IdentityResult result = await _userManager.CreateAsync(usuario, "123456");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(usuario, "Coordenador");
            }

            if (!professores.Any())
            {
                var usuario1 = new Usuario("Michael", "michael.professor", "michaelcdr@hotmail.com");
                var usuario2 = new Usuario("Pedro", "pedro", "teste@hotmail.com");

                IdentityResult result1 = await _userManager.CreateAsync(usuario1, "123456");
                if (result1.Succeeded)
                    await _userManager.AddToRoleAsync(usuario1, "Professor");
                
                IdentityResult result2 = await _userManager.CreateAsync(usuario2, "123456");
                if (result2.Succeeded)
                    await _userManager.AddToRoleAsync(usuario2, "Professor");
            }
        }

        private async Task CriarTipoDeUsuarioCoordenadorSeNaoExiste()
        {
            if (!await _roleManager.RoleExistsAsync("Coordenador"))
            {
                TipoUsuario role = new TipoUsuario { Name = "Coordenador" };
                IdentityResult result = await _roleManager.CreateAsync(role);
            }
        }

        private async Task CriaTipoDeUsuarioAlunoSeNaoExiste()
        {
            if (!await _roleManager.RoleExistsAsync("Aluno"))
            {
                TipoUsuario role = new TipoUsuario { Name = "Aluno" };
                IdentityResult result = await _roleManager.CreateAsync(role);
            }
        }

        private async Task CriaTipoDeUsuarioProfessorSeNaoExiste()
        {
            if (!await _roleManager.RoleExistsAsync("Professor"))
            {
                TipoUsuario role = new TipoUsuario { Name = "Professor" };
                IdentityResult result = await _roleManager.CreateAsync(role);
            }
        }
    }
}
