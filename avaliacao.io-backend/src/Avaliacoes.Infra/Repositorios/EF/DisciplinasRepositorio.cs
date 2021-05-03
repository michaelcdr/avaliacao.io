using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;

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
    }
}
