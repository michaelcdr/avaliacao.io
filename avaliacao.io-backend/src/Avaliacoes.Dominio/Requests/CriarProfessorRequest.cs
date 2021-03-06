using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class CriarProfessorRequest
    {
        public CriarProfessorRequest(string nome, string userName, string email, string senha)
        {
            Nome = nome;
            UserName = userName;
            Email = email;
            Senha = senha;
        }
        public CriarProfessorRequest()
        {

        }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<int> Disciplinas { get; set; }
    }
}
