using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs.Responses
{
    public class AppResponse
    {
        public List<string> Erros { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public AppResponse(bool sucesso, string mensagem, List<string> erros)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Erros = erros;
        }

        public AppResponse(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }
    }
}
