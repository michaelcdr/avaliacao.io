using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Requests;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services.Interfaces
{
    public interface IDisciplinaService
    {
        Task<AppResponse> ImportarDisciplinas(ImportarDisciplinas importarAlunos);
    }
}
