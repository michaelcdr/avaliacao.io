using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Usuario : IdentityUser
    {
        private List<string> _erros { get; set; }

        public Professor Professor { get; set; }
        public Aluno Aluno { get; set; }
        public Coordenador Coordenador { get; set; }
        public string Nome { get; set; }        

        public Usuario(string nome, string userName, string email)
        {
            this.Nome = nome;
            this.UserName = userName;
            this.Email = email;
        }

        public Usuario()
        {

        }

        public bool TaValido()
        {
            bool retorno = true;
            
            this._erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
            {
                _erros.Add("Nome não informado.");
                retorno = false;
            }

            if (string.IsNullOrEmpty(UserName))
            {
                _erros.Add("UserName não informado.");
                retorno = false;
            }

            return retorno;
        }

        public List<string> ObterErros()
        {
            return this._erros;
        }
    }
}
