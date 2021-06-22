using Avaliacoes.Dominio.Requests;
using System;

namespace Avaliacoes.Dominio.Entidades
{
    public class Coordenador 
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public void Atualizar(AtualizarCoordenadorRequest request)
        {
            this.Usuario.Nome = request.Nome;
            this.Usuario.Email = request.Email;
            this.Usuario.UserName = request.UserName;
        }
    }
}
