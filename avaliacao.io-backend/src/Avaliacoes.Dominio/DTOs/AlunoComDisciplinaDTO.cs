using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacoes.Dominio.DTOs
{
    public class AlunoComDisciplinaDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<int> Disciplinas { get; set; }
        public string Matricula { get; set; }

        public AlunoComDisciplinaDTO()
        {

        }

        public AlunoComDisciplinaDTO(Aluno aluno)
        {
            this.Email = aluno.Usuario.Email;
            this.Id = aluno.Usuario.Id;
            this.Nome = aluno.Usuario.Nome;
            this.UserName = aluno.Usuario.UserName;
            this.Disciplinas = new List<int>();
            this.Matricula = aluno.Matricula;

            if (aluno.Usuario.Aluno != null && aluno.Usuario.Aluno.Disciplinas != null)
                this.Disciplinas = aluno.Usuario.Aluno.Disciplinas.Select(e => e.Id).ToList();
        }

    }
}
