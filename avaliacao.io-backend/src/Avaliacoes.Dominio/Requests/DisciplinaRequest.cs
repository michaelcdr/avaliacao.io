using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class DisciplinaRequest
    {
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public string Horario { get; set; }
        public List<string> Professores { get; set; }

        public DisciplinaRequest()
        {
            Professores = new List<string>();
        }
    }
}
