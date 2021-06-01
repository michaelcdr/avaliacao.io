using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs
{
    public class DisciplinaDTO
    {
        public int Id { get; set; }
        public string Horario { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public List<string> Professores { get; set; }
    }
}
