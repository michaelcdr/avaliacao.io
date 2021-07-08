using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs
{
    public class HabilidadeComNotasDTO 
    {
        public int HabilidadeId { get; set; }
        public string Nome { get; set; }
        public List<NotasDTO> NotasDTO { get; set; }
    }

    public class NotasDTO
    {
        public string Dimensao { get; set; }
        public int DimensaoId { get; set; }
        public string Nota { get; set; }
    }
}
