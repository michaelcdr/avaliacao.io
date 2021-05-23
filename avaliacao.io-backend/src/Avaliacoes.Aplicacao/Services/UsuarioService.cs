using Avaliacoes.Aplicacao.Helpers;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private const string ERRO_BASE = "Não foi possivel criar o professor.";
        private const string MSG_SUCESSO = "Professor criado com sucesso.";
        private const string ROLE_PROFESSOR = "Professor";
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _uow;

        public UsuarioService(IUnitOfWork uow, UserManager<Usuario> userManager )
        {
            this._userManager = userManager;
            this._uow = uow;
        }

        public async Task<CriarProfessorResponse> CriarProfessor(CriarProfessorRequest request)
        {
            var usuario = new Usuario(request.Nome, request.UserName, request.Email);

            if (!usuario.TaValido())
                return new CriarProfessorResponse(false, ERRO_BASE, usuario.ObterErros());

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);

            if (!result.Succeeded)
                return new CriarProfessorResponse(false, ERRO_BASE, IdentityHelper.ObterErros(result));
            else
            {
                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_PROFESSOR);

                if (!resultRole.Succeeded) return new CriarProfessorResponse(false, ERRO_BASE, IdentityHelper.ObterErros(result));

                usuario.Professor = new Professor { UsuarioId = usuario.Id };
                await _uow.CommitAsync();
            }

            // vinculando disciplinas no professor
            List<Disciplina> disciplinasInformadas = await _uow.Disciplinas.ObterTodas(request.Disciplinas); 

            foreach (Disciplina disciplinaInformada in disciplinasInformadas)
                disciplinaInformada.AdicionarProfessor(usuario.Professor);
            
            await _uow.CommitAsync();

            return new CriarProfessorResponse(true, MSG_SUCESSO);
        }
    }
}
