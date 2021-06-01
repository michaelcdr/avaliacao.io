using System.Collections.Generic;

namespace Avaliacoes.Dominio.InputModels
{
    public class AtualizarProfessorRequest
    {
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<int> Disciplinas { get; set; }
        public string Id { get; set; }
    }
}
