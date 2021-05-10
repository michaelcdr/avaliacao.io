using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Avaliacoes.Infra.Repositorios.EF
{
    public class UsuariosRepositorio : Repositorio<Usuario>, IUsuariosRepositorio
    {
        public UsuariosRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<List<Usuario>> ObterProfessores()
        {
            return await ApplicationDbContext.Usuarios
                .Where(e => e.Tipo == "Professor")
                .Include(e => e.Professor).ToListAsync();
        }

        public async Task<Professor> ObterProfessor(int id)
        {
            return await ApplicationDbContext.Usuarios.Include(usuario => usuario.Professor).ThenInclude(e=>e.Usuario)
                .Include(e=>e.Professor).ThenInclude(p => p.Disciplinas)
                .Where(usuario => usuario.Id == id && usuario.Tipo == "Professor").Select(usuario => usuario.Professor)
                .SingleOrDefaultAsync();
        }
    }
}
