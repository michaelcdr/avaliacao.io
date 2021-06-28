using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Infra.Data.EF.TableMaping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Avaliacoes.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, TipoUsuario, string>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Competencia> Competencias { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<Dimensao> Dimensoes { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        //public ApplicationDbContext() {       } // SqlServer Tem que ter

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Disciplina>().Ignore(e => e._erros);
            builder.Entity<Competencia>().Ignore(e => e._erros);
            builder.Entity<Habilidade>().Ignore(e => e._erros);
            builder.Entity<Dimensao>().Ignore(e => e._erros);
            builder.Entity<Avaliacao>().Ignore(e => e._erros);

            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new TipoUsuarioMap());
            
            builder.ApplyConfiguration(new ProfessorMap());
            builder.ApplyConfiguration(new AlunoMap());
            builder.ApplyConfiguration(new CoordenadorMap());

            builder.ApplyConfiguration(new DisciplinaMap());
            builder.ApplyConfiguration(new CompetenciaMap());
            builder.ApplyConfiguration(new HabilidadeMap());
            builder.ApplyConfiguration(new DimensaoMap());
        }
    }
}
