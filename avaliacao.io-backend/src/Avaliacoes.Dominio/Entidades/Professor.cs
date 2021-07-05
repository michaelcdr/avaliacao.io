using Avaliacoes.Dominio.Requests;
using System;
using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Professor 
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Disciplina> Disciplinas { get; set; }

        public void Atualizar(string nome, string email, string userName)
        {
            Usuario.Nome = nome;
            Usuario.Email = email;
            Usuario.UserName = userName;
        }
    }
}
