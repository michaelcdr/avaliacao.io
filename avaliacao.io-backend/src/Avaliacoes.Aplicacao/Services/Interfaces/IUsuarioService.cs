using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.InputModels;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public interface IUsuarioService
    {
        Task<CriarProfessorResponse> CriarProfessor(CriarProfessorRequest request);
        Task<CriarCoordenadorResponse> CriarCoordenador(CriarCoordenadorRequest criarCoordenadorRequest);
        Task<CriarAlunoResponse> CriarAluno(CriarAlunoRequest request);
        Task<AtualizarProfessorResponse> AtualizarProfessor(AtualizarProfessorRequest request);
    }
}
