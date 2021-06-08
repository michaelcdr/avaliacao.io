using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class HabilidadeRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public int CompetenciaId { get; set; }

        public List<DimensaoRequest> Dimensoes { get; set; }
    }
}
