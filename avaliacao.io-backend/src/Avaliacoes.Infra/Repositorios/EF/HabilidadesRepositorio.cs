using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacoes.Infra.Repositorios.EF
{
    public class HabilidadesRepositorio : Repositorio<Habilidade>, IHabilidadesRepositorio
    {
        public HabilidadesRepositorio(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<bool> Existe(int competenciaId, string nome, int? habilidadeId)
        {
            return habilidadeId == null
                 ? await Context.Set<Habilidade>().AnyAsync(e => e.CompetenciaId == competenciaId && e.Nome == nome)
                 : await Context.Set<Habilidade>().AnyAsync(e => e.CompetenciaId == competenciaId && e.Nome == nome && e.Id != habilidadeId);
        }

        public async Task<IList<Habilidade>> ObterTodasComDimensoes()
        {
            return await Context.Set<Habilidade>().Include(e => e.Dimensoes).ToListAsync();
        }

        public async Task<Habilidade> ObterComDimensoes(int habilidadeId)
        {
            return await Context.Set<Habilidade>().Where(e => e.Id == habilidadeId).Include(e => e.Dimensoes).SingleOrDefaultAsync();
        }

        public async Task<IList<Habilidade>> ObterTodasPorCompetencia(int competenciaId)
        {
            return await Context.Set<Habilidade>().Where(e => e.CompetenciaId == competenciaId).Include(e => e.Dimensoes).ToListAsync();
        }
    }
}
