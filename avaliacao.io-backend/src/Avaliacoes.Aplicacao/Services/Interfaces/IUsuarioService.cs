using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.InputModels;
using Avaliacoes.Dominio.Requests;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public interface IUsuarioService
    {
        Task<AppResponse> CriarProfessor(CriarProfessorRequest request);
        Task<AppResponse> CriarCoordenador(CriarCoordenadorRequest criarCoordenadorRequest);
        Task<AppResponse> CriarAluno(CriarAlunoRequest request);
        Task<AppResponse> AtualizarProfessor(AtualizarProfessorRequest request);
        Task<AppResponse> VincularDisciplinasEmAluno(VincularAlunoDisciplinasRequest request);
        Task<AppResponse> AtualizarAluno(AtualizarAlunoRequest request);
        Task<AppResponse> AtualizarCoordenador(AtualizarCoordenadorRequest request);
    }
}
