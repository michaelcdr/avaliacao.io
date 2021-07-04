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
        private const string MSG_ERRO_UPDATE_ALUNO = "Informações de aluno atualizadas porem não foi possivel atualizar a senha.";
        private const string MSG_ERRO_UPDATE_PROF = "Informações de professor atualizadas porem não foi possivel atualizar a senha.";
        private const string MSG_ERRO_PWD_COORDENADOR = "Informações de coordenador atualizadas porem não foi possivel atualizar a senha.";
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

        public async Task<AppResponse> AtualizarCoordenador(AtualizarCoordenadorRequest request)
        {
            Coordenador coordenador = await _uow.Usuarios.ObterCoordenador(request.Id);

            if (string.IsNullOrEmpty(request.Senha)) coordenador.Usuario.AdicionarErro("Informe a nova senha.");

            if (string.IsNullOrEmpty(request.SenhaAntiga)) coordenador.Usuario.AdicionarErro("Informe a senha antiga.");

            if (!coordenador.Usuario.TaValido()) return new AppResponse(false, ERRO_BASE, coordenador.Usuario.ObterErros());

            coordenador.Atualizar(request); 
            await _uow.CommitAsync();

            IdentityResult result = await _userManager.ChangePasswordAsync(coordenador.Usuario, request.SenhaAntiga, request.Senha);

            if (!result.Succeeded)
                return new AppResponse(MSG_ERRO_PWD_COORDENADOR, false, IdentityHelper.ObterErros(result) );

            return new AppResponse(true, MSG_UPDATE_SUCESSO);
        }

        public async Task<AppResponse> AtualizarProfessor(AtualizarProfessorRequest request)
        {
            Professor professor = await _uow.Usuarios.ObterProfessor(request.Id);

            if (string.IsNullOrEmpty(request.Senha))
                professor.Usuario.AdicionarErro("Informe a nova senha.");

            if (string.IsNullOrEmpty(request.SenhaAntiga))
                professor.Usuario.AdicionarErro("Informe a senha antiga.");

            if (!professor.Usuario.TaValido()) return new AppResponse(false, ERRO_BASE, professor.Usuario.ObterErros());

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

            IdentityResult result = await _userManager.ChangePasswordAsync(professor.Usuario, request.SenhaAntiga, request.Senha);

            if (!result.Succeeded)
                return new AppResponse(MSG_ERRO_UPDATE_PROF, false, IdentityHelper.ObterErros(result));

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
                return new AppResponse(MSG_ERRO_UPDATE_ALUNO, false, IdentityHelper.ObterErros(result));

            return new AppResponse(true, MSG_UPDATE_ALUNO_SUCESSO, new AlunoComDisciplinaDTO(aluno));
        }
        
        public async Task<AppResponse> AvaliarAluno(AvaliarAluno request)
        {
            Aluno aluno = await _uow.Usuarios.ObterAluno(request.UsuarioId);

            if (aluno == null) return new AppResponse(false, "Aluno não encontrado.");

            Dimensao dimensao = await _uow.Dimensoes.Get(request.IdDimensao);

            if (dimensao == null) return new AppResponse(false, "Dimensão não encontrada.");

            Avaliacao avaliacao = await _uow.Usuarios.ObterAvaliacaoAluno(request.IdDimensao, aluno.Id, request.Semestre);

            if (avaliacao == null) 
            {
                avaliacao = new Avaliacao(request.IdDimensao, aluno.Id, request.Nota, request.Semestre);

                if (!avaliacao.TaValido()) return new AppResponse(false, "Não foi possivel avaliar o aluno.", avaliacao.ObterErros());

                _uow.Usuarios.AvaliarAluno(avaliacao);
                await _uow.CommitAsync(); 
            }
            else
            {
                avaliacao.AtualizarNota(request.Nota);
                await _uow.CommitAsync();
            }

            return new AppResponse(true, "Aluno avaliado com sucesso.");
        }

        private string FormatarNota(int nota)
        {
            if (nota == 0)
                return "Insuficiente";
            else if (nota == 1)
                return "Aptidão";
            else 
                return "Aptidão Plena";
        }

        public async Task<AppResponse> ObterGradeCurricular(string id, string login)
        {
            Aluno aluno = await _uow.Usuarios.ObterAluno(id);

            if (aluno == null) return new AppResponse(false, "Aluno não encontrado.");

            var disciplinasDTO = new List<DisciplinaAvaliadaDTO>();

            foreach (var disciplina in aluno.Disciplinas)
            {
                var disciplinaDTO = new DisciplinaAvaliadaDTO { Nome = disciplina.Nome, DisciplinaId = disciplina.Id };
                var competenciasDTO = new List<CompetenciaAvaliadaDTO>();

                foreach (var competencia in disciplina.Competencias)
                {
                    var comptenciaDto = new CompetenciaAvaliadaDTO { Nome = competencia.Nome, CompentenciaId = competencia.Id };
                    var habilidadesDTO = new List<HabilidadeAvaliadaDTO>();

                    foreach (var habilidade in competencia.Habilidades)
                    {
                        var habilidadeDTO = new HabilidadeAvaliadaDTO
                        {
                            Nome = habilidade.Nome,
                            HabilidadeId = habilidade.Id
                        };

                        var dimensoesDTo = new List<DimensaoAvaliadaDTO>();

                        foreach (var dimensao in habilidade.Dimensoes)
                        {
                            var dimensoesAvaliadas = aluno.Avaliacoes.Where(e => e.DimensaoId == dimensao.Id)
                                .Select(e => new DimensaoAvaliadaDTO { 
                                    AvaliacaoId = e.Id, 
                                    Nome = dimensao.Nome,
                                    Nota = FormatarNota(e.Nota), 
                                    Semestre = e.Semestre, 
                                    DimensaoId = e.DimensaoId,
                                    Data = e.DataAvaliacao.ToString("dd/MM/yyyy")
                                }).OrderBy(e => e.Semestre).ToList();

                            dimensoesDTo.AddRange(dimensoesAvaliadas);
                        }

                        habilidadeDTO.Dimensoes = dimensoesDTo;

                        habilidadesDTO.Add(habilidadeDTO);
                    }

                    comptenciaDto.Habilidades = habilidadesDTO;

                    competenciasDTO.Add(comptenciaDto);
                }

                disciplinaDTO.Competencias = competenciasDTO;

                disciplinasDTO.Add(disciplinaDTO);
            }

            var gradeCurricular = new GradeCurricular
            {
                NomeAluno = aluno.Usuario.Nome,
                Disciplinas = disciplinasDTO,
                UsuarioId = aluno.UsuarioId
            };
            return new AppResponse(true, "Grade curricular obtidada com sucesso", gradeCurricular);
        }

        public async Task<AppResponse> ObterNotas(string usuarioId, string name)
        {
            Aluno aluno = await _uow.Usuarios.ObterAluno(usuarioId);

            if (aluno == null) return new AppResponse(false, "Aluno não encontrado.");

            var avaliacoes = new List<AvaliacaoDTO>();

            if (aluno.Avaliacoes.Count > 0)
            {
                avaliacoes = aluno.Avaliacoes.Select(e => new AvaliacaoDTO
                {
                    Nota = FormatarNota(e.Nota),
                    Disciplina = e.Dimensao.Habilidade.Competencia.Disciplina.Nome,
                    Competencia = e.Dimensao.Habilidade.Competencia.Nome,
                    Habilidade = e.Dimensao.Habilidade.Nome,
                    Dimensao = e.Dimensao.Nome,
                    Semestre = e.Semestre,
                    DisciplinaId = e.Dimensao.Habilidade.Competencia.DisciplinaId,
                    CompetenciaId = e.Dimensao.Habilidade.CompetenciaId,
                    HabilidadeId = e.Dimensao.Habilidade.Id,
                    DimensaoId = e.DimensaoId,
                    Data = e.DataAvaliacao.ToString("dd/MM/yyyy")

                }).ToList();
            }

            return new AppResponse(true, "Notas obtidas com sucesso.", avaliacoes);
        }

    }
}