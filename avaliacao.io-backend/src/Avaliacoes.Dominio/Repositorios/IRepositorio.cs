using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        void Add(TEntity entidade);

        Task<TEntity> Get(object id);
        void Delete(TEntity entidade);
        Task<IList<TEntity>> GetAll();
    }
}
