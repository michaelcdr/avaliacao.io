using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Dominio.Transacoes;
using Avaliacoes.Infra.Data;
using System.Threading.Tasks;

namespace Avaliacoes.Infra.Transacoes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IDisciplinasRepositorio Disciplinas { get; private set; }
        public IUsuariosRepositorio Usuarios { get; private set; }
        public ICompetenciasRepositorio Compentencias { get; private set; }
        public IHabilidadesRepositorio Habilidades { get; private set; }
        public  async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }


        public UnitOfWork(
            ApplicationDbContext context,
            IDisciplinasRepositorio disciplinasRepositorio,
            IUsuariosRepositorio usuariosRepositorio,
            ICompetenciasRepositorio competenciasRepositorio,
            IHabilidadesRepositorio habilidadesRepositorio)
        {
            this._context = context;
            this.Disciplinas = disciplinasRepositorio;
            this.Usuarios = usuariosRepositorio;
            this.Compentencias = competenciasRepositorio;
            this.Habilidades = habilidadesRepositorio;
        }
    }
}
