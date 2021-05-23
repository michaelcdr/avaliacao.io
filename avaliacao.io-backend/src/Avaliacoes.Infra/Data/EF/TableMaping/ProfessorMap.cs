using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class ProfessorMap : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {
            builder.ToTable("Professores");
            builder.HasMany(professor => professor.Disciplinas)
                   .WithMany(professor => professor.Professores);

            builder.HasOne(e => e.Usuario).WithOne(e=>e.Professor);
        }
    }
}
