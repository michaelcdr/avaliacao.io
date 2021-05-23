using System.Collections.Generic;

namespace Avaliacoes.Dominio.InputModels
{
    public class CriarProfessorRequest
    {
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<int> Disciplinas { get; set; }
    } 
}
