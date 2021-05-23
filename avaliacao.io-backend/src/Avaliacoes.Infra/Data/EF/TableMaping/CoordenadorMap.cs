using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class CoordenadorMap : IEntityTypeConfiguration<Coordenador>
    {
        public void Configure(EntityTypeBuilder<Coordenador> builder)
        {
            builder.ToTable("Coordenadores");
        }
    }
}
