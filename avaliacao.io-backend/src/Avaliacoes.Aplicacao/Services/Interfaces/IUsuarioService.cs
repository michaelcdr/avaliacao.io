using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.InputModels;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public interface IUsuarioService
    {
        Task<CriarProfessorResponse> CriarProfessor(CriarProfessorRequest request);
    }
}
