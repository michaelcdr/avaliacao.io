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

            builder.HasMany(disciplina => disciplina.Competencias)
                .WithOne(competencia => competencia.Disciplina)
                .HasForeignKey(e => e.DisciplinaId);
        }
    }

    public class CompetenciaMap : IEntityTypeConfiguration<Competencia>
    {
        public void Configure(EntityTypeBuilder<Competencia> builder)
        {
            builder.ToTable("Competencias");
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.Descritivo).IsRequired(true);

            builder.HasMany(competencia => competencia.Habilidades)
               .WithOne(habilidade => habilidade.Competencia)
               .HasForeignKey(e => e.CompetenciaId);
        }
    }

    public class HabilidadeMap : IEntityTypeConfiguration<Habilidade>
    {
        public void Configure(EntityTypeBuilder<Habilidade> builder)
        {
            builder.ToTable("Habilidades");
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(p => p.Descritivo).IsRequired(true);

            builder.HasMany(habilidade => habilidade.Dimensoes)
               .WithOne(dimensao => dimensao.Habilidade)
               .HasForeignKey(e => e.HabilidadeId);
        }
    }

    public class DimensaoMap : IEntityTypeConfiguration<Dimensao>
    {
        public void Configure(EntityTypeBuilder<Dimensao> builder)
        {
            builder.ToTable("Dimensoes");
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired(true);
        }
    }
}
