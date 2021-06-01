using Avaliacoes.Dominio.Repositorios;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Transacoes
{
    public interface IUnitOfWork
    {
        IDisciplinasRepositorio Disciplinas { get; }
        ICompetenciasRepositorio Compentencias { get; }
        IUsuariosRepositorio Usuarios { get; }
        Task<int> CommitAsync();
    }
}
