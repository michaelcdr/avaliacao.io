using Avaliacoes.Dominio.Requests;
using System;
using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Aluno
    {
        public string Matricula { get; set; }
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Disciplina> Disciplinas { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public void Atualizar(AtualizarAlunoRequest request)
        {
            Usuario.Nome = request.Nome;
            Usuario.Email = request.Email;
            Usuario.UserName = request.UserName;
            Matricula = request.Matricula;
        }
    } 
}
