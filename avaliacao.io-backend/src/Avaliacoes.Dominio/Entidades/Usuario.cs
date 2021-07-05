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
            this._erros = this._erros == null ? new List<string>() : this._erros;

            if (string.IsNullOrEmpty(Nome))
                _erros.Add("Nome não informado.");

            if (string.IsNullOrEmpty(UserName))
                _erros.Add("UserName não informado.");

            if (string.IsNullOrEmpty(Email))
                _erros.Add("E-mail não informado.");

            return this._erros.Count == 0;
        }

        public List<string> ObterErros()
        {
            return this._erros;
        }

        public void AdicionarErro(string erro)
        {
            if (this._erros == null)
                this._erros = new List<string>();

            this._erros.Add(erro);
        }
    }
}
