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

        public async Task<Disciplina> ObterComProfessores(int id)
        {
            return await Context.Set<Disciplina>().Include(e => e.Professores).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Disciplina>> ObterTodas(List<int> disciplinas)
        {
            return await Context.Set<Disciplina>().Where(e => disciplinas.Contains(e.Id)).ToListAsync();
        }

        public async Task<List<Disciplina>> ObterTodasComProfessores()
        {
            return await Context.Set<Disciplina>().Include(e => e.Professores).ToListAsync();
        }
    }

    public class CompetenciasRepositorio : Repositorio<Competencia>, ICompetenciasRepositorio
    {
        public CompetenciasRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<IList<Competencia>> ObterTodasPorDisciplina(int idDisciplina)
        {
            return await Context.Set<Competencia>().Where(e => e.DisciplinaId == idDisciplina).ToListAsync();
        }
    }
}
