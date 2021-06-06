using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class CriarAlunoRequest
    {
        public CriarAlunoRequest(string nome, string userName, string email, string senha, List<int> disciplinas)
        {
            Nome = nome;
            UserName = userName;
            Email = email;
            Senha = senha;

            Disciplinas = disciplinas == null ? new List<int>() : disciplinas;
        }
        public CriarAlunoRequest()
        {

        }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public List<int> Disciplinas { get; set; }
        public string Matricula { get; set; }
    }
}
