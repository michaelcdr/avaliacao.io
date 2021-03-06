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

            //coordenadores 

            if (!coordenadores.Any(e => e.UserName == "michael"))
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Michael", "michael", "michaelcdr@hotmail.com", "123456"));

            if (!coordenadores.Any(e => e.UserName == "michael.coordenador"))
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Michael", "michael.coordenador", "michaelcdr@hotmail.com", "123456"));

            if (!coordenadores.Any(e => e.UserName == "bruno.coordenador"))
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Bruno", "bruno.coordenador", "bruno@hotmail.com", "123456"));

            if (!coordenadores.Any(e => e.UserName == "pedro.coordenador"))
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Pedro", "pedro.coordenador", "pedro@hotmail.com", "123456"));

            if (!coordenadores.Any(e => e.UserName == "taciano.coordenador"))
                await _usuarioService.CriarCoordenador(new CriarCoordenadorRequest("Taciano", "taciano.coordenador", "taciano@hotmail.com", "123456"));

            //professores 

            if (!professores.Any(e => e.UserName == "michael.professor"))
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Michael", "michael.professor", "michaelcdr@hotmail.com", "123456"));
            
            if (!professores.Any(e => e.UserName == "bruno.professor"))
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Bruno", "bruno.professor", "bruno@hotmail.com", "123456"));

            if (!professores.Any(e => e.UserName == "pedro.professor"))
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Pedro", "pedro.professor", "pedro@hotmail.com", "123456"));

            if (!professores.Any(e => e.UserName == "taciano.professor"))
                await _usuarioService.CriarProfessor(new CriarProfessorRequest("Taciano", "taciano.professor", "taciano@hotmail.com", "123456"));

            //alunos

            if (!alunos.Any(e=> e.UserName == "michael.aluno"))
                await _usuarioService.CriarAluno(new CriarAlunoRequest("Michael", "michael.aluno", "michaelcdr@hotmail.com", "123456", new List<int>()) { Matricula = "123" });

            if (!alunos.Any(e => e.UserName == "bruno.aluno"))
                await _usuarioService.CriarAluno(new CriarAlunoRequest("Bruno", "bruno.aluno", "bruno@hotmail.com", "123456", new List<int>()) { Matricula = "123" });

            if (!alunos.Any(e => e.UserName == "taciano.aluno"))
                await _usuarioService.CriarAluno(new CriarAlunoRequest("Taciano", "taciano.aluno", "taciano@hotmail.com", "123456", new List<int>()) { Matricula = "123" });

            if (!alunos.Any(e => e.UserName == "pedro.aluno"))
                await _usuarioService.CriarAluno(new CriarAlunoRequest("Pedro", "pedro.aluno", "pedro@hotmail.com", "123456", new List<int>()) { Matricula = "123" });
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
