using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Infra.Repositorios.EF
{
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

        public async Task<bool> Existe(int idDisciplina, string nome, int? idCompetencia)
        {
            return idCompetencia == null
                ? await Context.Set<Competencia>().AnyAsync(e => e.Id == idDisciplina && e.Nome == nome)
                : await Context.Set<Competencia>().AnyAsync(e => e.Id == idDisciplina && e.Nome == nome && e.Id != idCompetencia);
        } 
    }
}
