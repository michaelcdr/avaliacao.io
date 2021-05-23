using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Infra.Repositorios.EF
{
    public class DisciplinasRepositorio : Repositorio<Disciplina>, IDisciplinasRepositorio
    {
        public DisciplinasRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<List<Disciplina>> ObterTodas(List<int> disciplinas)
        {
            return await Context.Set<Disciplina>().Where(e => disciplinas.Contains(e.Id)).ToListAsync();
        }
    }
}
