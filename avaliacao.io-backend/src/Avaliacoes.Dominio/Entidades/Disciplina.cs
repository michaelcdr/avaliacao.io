using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacoes.Dominio.Entidades
{
    public class Disciplina : EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public string Horario { get; set; }
        public List<Professor> Professores { get; set; }
        public List<Aluno> Alunos { get; set; }
        public List<Competencia> Competencias { get; set; }
        
        public Disciplina(string nome, string descritivo, string horario)
        {
            this.Nome = nome;
            this.Horario = horario;
            this.Descritivo = descritivo;
        }

        public void AdicionarProfessores(List<Professor> professores)
        {
            if (this.Professores == null)
                this.Professores = new List<Professor>();

            foreach (var professor in professores)
                this.Professores.Add(professor);
        }

        public void AdicionarProfessor(Professor professor)
        {
            if (this.Professores == null)
                this.Professores = new List<Professor>();

            this.Professores.Add(professor);
        }

        public void AdicionaAluno(Aluno aluno)
        {
            if (this.Alunos == null)
                this.Alunos = new List<Aluno>();

            this.Alunos.Add(aluno);
        }

        public void RemoverProfessor(Professor professor)
        {
            if (this.Professores != null)
            {
                this.Professores.Remove(professor);
            }
        }
        public DisciplinaDTO ToDTO()
        {
            return new DisciplinaDTO
            {
                Descritivo = this.Descritivo,
                Nome = this.Nome,
                Horario = this.Horario,
                Id = this.Id,
                Professores = this.Professores == null
                    ? new List<string>()
                    : this.Professores.Select(e => e.UsuarioId).ToList()
            };
        }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this.AdicionarErro("Informe o nome.", "Nome");

            if (string.IsNullOrEmpty(this.Horario))
                this.AdicionarErro("Informe o horário.","Horario");

            return this._erros.Count == 0;
        }

        public void Atualizar(DisciplinaRequest disciplinaDTO)
        {
            this.Descritivo = disciplinaDTO.Descritivo;
            this.Nome = disciplinaDTO.Nome;
            this.Horario = disciplinaDTO.Horario;
            this.Professores = new List<Professor>();
        }

        public void RemoverAluno(Aluno aluno)
        {
            if (this.Alunos != null)
                this.Alunos.Remove(aluno);
        }
    }
}
