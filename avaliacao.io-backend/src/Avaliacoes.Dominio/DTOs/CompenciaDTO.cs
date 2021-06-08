using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacoes.Dominio.DTOs
{
    public class CompenciaDTO
    {
        public CompenciaDTO(int id, string nome, string descritivo, int disciplinaId)
        {
            Id = id;
            Nome = nome;
            Descritivo = descritivo;
            DisciplinaId = disciplinaId;
        }

        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public string Descritivo { get; set; }
        public string Nome { get; set; }
    }

    public class HabilidadeDTO
    {
        public HabilidadeDTO(int id, string nome, string descritivo, int competenciaId, List<Dimensao> dimensoes)
        {
            Id = id;
            Nome = nome;
            Descritivo = descritivo;
            CompetenciaId = competenciaId;

            this.Dimensoes = dimensoes == null
                ? new List<DimensaoDTO>()
                : dimensoes.Select(e => e.ToDTO()).ToList();
        }

        public int Id { get; set; }
        public int CompetenciaId { get; set; }
        public string Descritivo { get; set; }
        public string Nome { get; set; }

        public List<DimensaoDTO> Dimensoes { get; set; }
    }
}
