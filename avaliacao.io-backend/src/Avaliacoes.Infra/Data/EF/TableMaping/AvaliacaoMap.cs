using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class AvaliacaoMap : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.ToTable("Avaliacoes");

            builder.Property(e => e.Nota).IsRequired(true);
            builder.Property(e => e.Semestre).IsRequired(true);
            builder.HasOne(e => e.Aluno).WithMany(e => e.Avaliacoes);
        }
    }
}
