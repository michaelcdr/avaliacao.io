using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Requests
{
    public class CompetenciaRequest
    {
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public int DisciplinaId { get; set; }
    }
}
