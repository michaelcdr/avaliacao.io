using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs
{
    public class ProfessorDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public List<int> Disciplinas { get; set; }
    }
}
