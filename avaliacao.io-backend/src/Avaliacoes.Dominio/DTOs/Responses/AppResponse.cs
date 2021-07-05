using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs.Responses
{
    public class AppResponse
    {
        public List<string> Erros { get; private set; }
        public bool Sucesso { get; private set; }
        public string Mensagem { get; private set; }
        public object Dados { get; private set; }
        public AppResponse()
        {

        }
        public AppResponse(string mensagem, bool sucesso,  List<string> erros)
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
        public AppResponse(bool sucesso, string mensagem, object dados)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Dados = dados;
        }
    }
}
