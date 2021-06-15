using Avaliacoes.Aplicacao.Helpers;
using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public class UsuarioService : IUsuarioService
    {
        private const string ERRO_BASE = "Não foi possivel criar o professor.";
        private const string ERRO_CRIAR_COORDENADOR = "Não foi possivel criar o professor.";
        private const string ERRO_CRIAR_ALUNO = "Não foi possivel criar o aluno.";
        private const string ERRO_ATUALIZAR_ALUNO = "Não foi possivel atualizar o aluno.";
        private const string MSG_CRIAR_COORDENADOR = "Coordenador criado com sucesso.";
        private const string MSG_CRIAR_ALUNO = "Aluno criado com sucesso.";
        private const string MSG_SUCESSO = "Professor criado com sucesso.";
        private const string MSG_UPDATE_SUCESSO = "Professor atualizado com sucesso.";
        private const string MSG_UPDATE_ALUNO_SUCESSO = "Aluno atualizado com sucesso.";
        private const string ROLE_PROFESSOR = "Professor";
        private const string ROLE_COORDENADOR = "Coordenador";
        private const string ROLE_ALUNO = "Aluno";
        private readonly UserManager<Usuario> _userManager;
        private readonly IUnitOfWork _uow;

        public UsuarioService(IUnitOfWork uow, UserManager<Usuario> userManager )
        {
            this._userManager = userManager;
            this._uow = uow;
        }

        public async Task<AppResponse> CriarAluno(CriarAlunoRequest request)
        {
            var usuario = new Usuario(request.Nome, request.UserName, request.Email);

            if (!usuario.TaValido()) return new AppResponse(false, ERRO_CRIAR_ALUNO, usuario.ObterErros());

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);

            if (!result.Succeeded)
                return new AppResponse(false, ERRO_CRIAR_ALUNO, IdentityHelper.ObterErros(result));
            else
            {
                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_ALUNO);

                if (!resultRole.Succeeded) return new AppResponse(false, ERRO_CRIAR_ALUNO, IdentityHelper.ObterErros(result));

                usuario.Aluno = new Aluno { UsuarioId = usuario.Id, Matricula = request.Matricula };
                await _uow.CommitAsync();

                await VincularDisciplinasEmAluno(new VincularAlunoDisciplinasRequest(request.Disciplinas, usuario.Id));
            }

            return new AppResponse(true, MSG_CRIAR_ALUNO, new AlunoComDisciplinaDTO(usuario.Aluno));
        }

        public async Task<AppResponse> VincularDisciplinasEmAluno(VincularAlunoDisciplinasRequest request)
        {
            // vinculando disciplinas no aluno
            if (request.IdsDisciplinas != null)
            {
                Aluno aluno = await _uow.Usuarios.ObterAluno(request.UsuarioId);

                List<Disciplina> disciplinasInformadas = await _uow.Disciplinas.ObterTodas(request.IdsDisciplinas);

                if (disciplinasInformadas != null)
                    foreach (Disciplina disciplinaInformada in disciplinasInformadas)
                        disciplinaInformada.AdicionaAluno(aluno);

                await _uow.CommitAsync();
            }
            return new AppResponse(true, "Alunos vinculados as disciplinas com sucesso.");
        }

        public async Task<AppResponse> CriarCoordenador(CriarCoordenadorRequest request)
        {
            var usuario = new Usuario(request.Nome, request.UserName, request.Email);

            if (!usuario.TaValido()) return new AppResponse(false, ERRO_CRIAR_COORDENADOR, usuario.ObterErros());

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);

            if (!result.Succeeded)
                return new AppResponse(false, ERRO_CRIAR_COORDENADOR, IdentityHelper.ObterErros(result));
            else
            {
                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_COORDENADOR);

                if (!resultRole.Succeeded) return new AppResponse(false, ERRO_CRIAR_COORDENADOR, IdentityHelper.ObterErros(result));

                usuario.Coordenador = new Coordenador { UsuarioId = usuario.Id };
                await _uow.CommitAsync();
            }
            return new AppResponse(true, MSG_CRIAR_COORDENADOR);
        }

        public async Task<AppResponse> CriarProfessor(CriarProfessorRequest request)
        {
            var usuario = new Usuario(request.Nome, request.UserName, request.Email);

            if (!usuario.TaValido())
                return new AppResponse(false, ERRO_BASE, usuario.ObterErros());

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);

            if (!result.Succeeded)
                return new AppResponse(false, ERRO_BASE, IdentityHelper.ObterErros(result));
            else
            {
                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_PROFESSOR);

                if (!resultRole.Succeeded) return new AppResponse(false, ERRO_BASE, IdentityHelper.ObterErros(result));

                usuario.Professor = new Professor { UsuarioId = usuario.Id };
                await _uow.CommitAsync();
            }

            // vinculando disciplinas no professor
            if (request.Disciplinas != null)
            {
                List<Disciplina> disciplinasInformadas = await _uow.Disciplinas.ObterTodas(request.Disciplinas);

                if (disciplinasInformadas != null)
                    foreach (Disciplina disciplinaInformada in disciplinasInformadas)
                        disciplinaInformada.AdicionarProfessor(usuario.Professor);

                await _uow.CommitAsync();
            }
            return new AppResponse(true, MSG_SUCESSO);
        }

        public async Task<AppResponse> AtualizarProfessor(AtualizarProfessorRequest request)
        {
            Professor professor = await _uow.Usuarios.ObterProfessor(request.Id);

            if (!professor.Usuario.TaValido())
                return new AppResponse(false, ERRO_BASE, professor.Usuario.ObterErros());

            professor.Usuario.Nome = request.Nome;
            professor.Usuario.Email = request.Email;
            professor.Usuario.UserName = request.UserName;

            // vinculando disciplinas no professor
            if (request.Disciplinas != null)
            {
                List<Disciplina> disciplinasInformadas = await _uow.Disciplinas.ObterTodas(request.Disciplinas);

                if (disciplinasInformadas != null)
                {
                    foreach (Disciplina disciplinaInformada in disciplinasInformadas)
                    {
                        if (!professor.Disciplinas.Any(e => e.Id == disciplinaInformada.Id))
                        {
                            disciplinaInformada.AdicionarProfessor(professor);
                        }
                    }

                    var idsRemover = professor.Disciplinas.Select(e => e.Id).ToList().Except(request.Disciplinas).ToList();

                    List<Disciplina> disciplinasRemover = professor.Disciplinas.Where(e => idsRemover.Contains(e.Id)).ToList();

                    foreach (var disciplinaInformada in disciplinasRemover)
                    {
                        disciplinaInformada.RemoverProfessor(professor);
                    }
                }
            }

            await _uow.CommitAsync();

            return new AppResponse(true, MSG_UPDATE_SUCESSO);
        }

        public async Task<AppResponse> AtualizarAluno(AtualizarAlunoRequest request)
        {
            Aluno aluno = await _uow.Usuarios.ObterAluno(request.Id);

            if (string.IsNullOrEmpty(request.Senha))
                aluno.Usuario.AdicionarErro("Informe a nova senha.");
            
            if (string.IsNullOrEmpty(request.SenhaAntiga))
                aluno.Usuario.AdicionarErro("Informe a senha antiga.");

            aluno.Atualizar(request);

            if (!aluno.Usuario.TaValido()) return new AppResponse(false, ERRO_ATUALIZAR_ALUNO, aluno.Usuario.ObterErros());
            
            // vinculando disciplinas no aluno
            if (request.Disciplinas != null)
            {
                List<Disciplina> disciplinasInformadas = await _uow.Disciplinas.ObterTodas(request.Disciplinas);

                if (disciplinasInformadas != null)
                {
                    foreach (Disciplina disciplinaInformada in disciplinasInformadas)
                        if (!aluno.Disciplinas.Any(e => e.Id == disciplinaInformada.Id))
                            disciplinaInformada.AdicionaAluno(aluno);

                    var idsRemover = aluno.Disciplinas.Select(e => e.Id).ToList().Except(request.Disciplinas).ToList();

                    List<Disciplina> disciplinasRemover = aluno.Disciplinas.Where(e => idsRemover.Contains(e.Id)).ToList();

                    foreach (var disciplinaInformada in disciplinasRemover)                    
                        disciplinaInformada.RemoverAluno(aluno);
                }
            }

            await _uow.CommitAsync();

            IdentityResult result = await _userManager.ChangePasswordAsync(aluno.Usuario, request.SenhaAntiga, request.Senha);

            if (!result.Succeeded)
            {
                List<string> erros = result.Errors.Select(e => e.Code + " - " + e.Description).ToList();

                return new AppResponse("Informações de aluno atualizadas porem não foi possivel atualizar a senha.", false, erros);
            }

            return new AppResponse(true, MSG_UPDATE_ALUNO_SUCESSO, new AlunoComDisciplinaDTO(aluno));
        }
    }
}
