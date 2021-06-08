using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface IHabilidadesRepositorio : IRepositorio<Habilidade>
    {
        Task<IList<Habilidade>> ObterTodasPorCompetencia(int competenciaId);
        Task<bool> Existe(int competenciaId, string descritivo, int? habilidadeId);
        Task<IList<Habilidade>> ObterTodasComDimensoes();
        Task<Habilidade> ObterComDimensoes(int habilidadeId);
    }
}
