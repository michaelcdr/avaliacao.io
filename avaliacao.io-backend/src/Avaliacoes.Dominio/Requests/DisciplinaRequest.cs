using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class DisciplinaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public List<int> Professores { get; set; }

        
    }
}
