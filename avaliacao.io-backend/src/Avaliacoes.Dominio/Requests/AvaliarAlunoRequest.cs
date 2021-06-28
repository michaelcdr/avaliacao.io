using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Requests
{
    public class AvaliarAluno
    {
        public string UsuarioId { get; set; }
        public int IdDimensao { get; set; }
        public int Nota { get; set; }
        public string Semestre { get; set; }
    }
}
