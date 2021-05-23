using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Infra.Data.EF.TableMaping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Avaliacoes.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, TipoUsuario, string>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        
        //public ApplicationDbContext() {       } // SqlServer Tem que ter

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new TipoUsuarioMap());
            builder.ApplyConfiguration(new ProfessorMap());
            builder.ApplyConfiguration(new AlunoMap());
            builder.ApplyConfiguration(new CoordenadorMap());
            builder.ApplyConfiguration(new DisciplinaMap());
        }
    }
}
