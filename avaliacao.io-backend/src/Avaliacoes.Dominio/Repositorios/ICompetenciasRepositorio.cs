using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface ICompetenciasRepositorio : IRepositorio<Competencia>
    {
        Task<bool> Existe(int idDisciplina, string nome);
        Task<IList<Competencia>> ObterTodasPorDisciplina(int idCompetencia);
    }
}
