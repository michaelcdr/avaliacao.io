using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface IDisciplinasRepositorio : IRepositorio<Disciplina>
    {
        Task<List<Disciplina>> ObterTodas(List<int> disciplinas);
        Task<List<Disciplina>> ObterTodasComProfessores();
        Task<Disciplina> ObterComProfessores(int id);
    }
}
