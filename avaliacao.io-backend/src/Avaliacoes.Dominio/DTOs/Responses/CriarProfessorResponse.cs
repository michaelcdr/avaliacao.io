using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs.Responses
{
    public class CriarProfessorResponse  : ResponseBase 
    {
        public CriarProfessorResponse(bool sucesso, string mensagem,List<string> erros):base(sucesso, mensagem, erros)
        {

        }
        public CriarProfessorResponse(bool sucesso, string mensagem) : base(sucesso, mensagem)
        {

        }
    }
    public class CriarCoordenadorResponse : ResponseBase
    {
        public CriarCoordenadorResponse(bool sucesso, string mensagem, List<string> erros) : base(sucesso, mensagem, erros)
        {

        }
        public CriarCoordenadorResponse(bool sucesso, string mensagem) : base(sucesso, mensagem)
        {

        }
    }

    public class ResponseBase 
    {
        public List<string> Erros { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public ResponseBase(bool sucesso, string mensagem, List<string> erros)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
            this.Erros = erros;
        }

        public ResponseBase(bool sucesso, string mensagem)
        {
            this.Sucesso = sucesso;
            this.Mensagem = mensagem;
        }
    }
}
