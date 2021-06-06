using Avaliacoes.Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Requests;

namespace Avaliacoes.Aplicacao.Services
{
    public class SeedService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<TipoUsuario> _roleManager;
        private readonly IUsuarioService _usuarioService;

        public SeedService(
            UserManager<Usuario> userManager,
            RoleManager<TipoUsuario> roleManager, 
            IUsuarioService usuarioService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioService = usuarioService;
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
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Michael", "michael", "michaelcdr@hotmail.com", "123456"));

            if (!professores.Any())
            {
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Michael", "michael.professor", "michaelcdr@hotmail.com", "123456"));
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Pedro", "pedro", "teste@hotmail.com", "123456"));
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
