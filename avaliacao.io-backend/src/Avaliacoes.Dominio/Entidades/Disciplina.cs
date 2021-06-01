using Avaliacoes.Dominio.DTOs;
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
        public List<Competencia> Competencias { get; set; }
        
        public Disciplina(string nome, string descritivo)
        {
            this.Nome = nome;
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
                this.AdicionarErro("Informe o nome.", Nome);

            if (string.IsNullOrEmpty(this.Horario))
                this.AdicionarErro("Informe o horário.",Horario);

            return this._erros.Count == 0;
        }
    }
}
