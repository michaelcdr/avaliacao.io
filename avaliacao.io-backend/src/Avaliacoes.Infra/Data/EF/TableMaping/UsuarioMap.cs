using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.Property(p => p.Nome).HasMaxLength(150).IsRequired(true);
            builder.Property(p => p.Email).HasMaxLength(100).IsRequired(true);
            builder.HasOne(e => e.Professor).WithOne(e => e.Usuario).HasForeignKey<Professor>(e => e.UsuarioId);
            builder.HasOne(e => e.Aluno).WithOne(e => e.Usuario).HasForeignKey<Aluno>(e => e.UsuarioId);
            builder.HasOne(e => e.Coordenador).WithOne(e => e.Usuario).HasForeignKey<Coordenador>(e => e.UsuarioId);
        }
    }
}
