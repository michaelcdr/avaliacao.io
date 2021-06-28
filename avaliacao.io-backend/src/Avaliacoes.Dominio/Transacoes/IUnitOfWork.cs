using Avaliacoes.Dominio.Repositorios;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Transacoes
{
    public interface IUnitOfWork
    {
        IDisciplinasRepositorio Disciplinas { get; }
        ICompetenciasRepositorio Compentencias { get; }
        IHabilidadesRepositorio Habilidades { get;  }
        IUsuariosRepositorio Usuarios { get; }
        IDimensoesRepositorio Dimensoes { get; }
        Task<int> CommitAsync();
    }
}
