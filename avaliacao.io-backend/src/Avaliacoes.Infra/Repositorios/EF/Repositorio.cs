using Avaliacoes.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Infra.Repositorios.EF
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repositorio(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entidade)
        {
            Context.Set<TEntity>().Add(entidade);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> Get(object id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
    }
}
