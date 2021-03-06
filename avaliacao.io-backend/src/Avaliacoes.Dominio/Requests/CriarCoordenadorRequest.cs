using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class CriarCoordenadorRequest
    {
        public CriarCoordenadorRequest(string nome, string userName, string email, string senha)
        {
            Nome = nome;
            UserName = userName;
            Email = email;
            Senha = senha;
        }

        public CriarCoordenadorRequest()
        {

        }

        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } 
    }

    public class AtualizarCoordenadorRequest
    {
        public AtualizarCoordenadorRequest(string nome, string userName, string email, string senha)
        {
            Nome = nome;
            UserName = userName;
            Email = email;
            Senha = senha;
        }

        public AtualizarCoordenadorRequest()
        {

        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string SenhaAntiga { get; set; }
    }
}
