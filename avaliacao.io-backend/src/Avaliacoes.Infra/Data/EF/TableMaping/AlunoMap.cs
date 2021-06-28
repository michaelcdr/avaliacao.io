using Avaliacoes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Avaliacoes.Infra.Data.EF.TableMaping
{
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Alunos");

            builder.Property(e => e.Matricula).HasMaxLength(100).IsRequired(true);

            builder.HasMany(e => e.Disciplinas)
                   .WithMany(e => e.Alunos)
                   .UsingEntity(j => j.ToTable("DisciplinasAlunos"));

            builder.HasMany(e => e.Avaliacoes).WithOne(e => e.Aluno);
        }
    }
}
