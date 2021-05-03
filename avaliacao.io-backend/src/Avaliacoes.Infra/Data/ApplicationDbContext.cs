using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Avaliacoes.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        
        
        //public ApplicationDbContext() {       } // SqlServer Tem que ter
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().HasKey(e => e.Id);
            builder.Entity<Usuario>().ToTable("Usuarios");
            builder.Entity<Usuario>().Property(p => p.Nome).HasMaxLength(150).IsRequired(true);
            builder.Entity<Usuario>().Property(p => p.Email).HasMaxLength(100).IsRequired(true);
            builder.Entity<Usuario>().Property(p => p.Senha).HasMaxLength(100).IsRequired(true);

            builder.Entity<Professor>().ToTable("Professores");

            builder.Entity<Disciplina>().ToTable("Disciplinas");
            builder.Entity<Disciplina>().Property(p => p.Nome).HasMaxLength(100).IsRequired(true);
            builder.Entity<Disciplina>().Property(p => p.Descritivo).IsRequired(true);
            builder.Entity<Disciplina>()
                .HasMany(disciplina => disciplina.Professores)
                .WithMany(professor => professor.Disciplinas);

            //modelBuilder.Entity<Disciplina>().Ignore(e => e.Erros);
        }
    }
}
