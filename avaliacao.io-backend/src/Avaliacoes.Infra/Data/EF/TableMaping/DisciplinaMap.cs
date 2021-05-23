using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class DisciplinaMap : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas");
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.Descritivo).IsRequired(true);
            builder.HasMany(disciplina => disciplina.Professores)
                   .WithMany(professor => professor.Disciplinas);
        }
    }
}
