using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Avaliacoes.Dominio.DTOs
{
    public class ProfessorComDisciplinaDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<int> Disciplinas { get; set; }


        public ProfessorComDisciplinaDTO()
        {

        }

        public ProfessorComDisciplinaDTO(Usuario usuario)
        {
            this.Email = usuario.Email;
            this.Id = usuario.Id;
            this.Nome = usuario.Nome;
            this.UserName = usuario.UserName;
            this.Disciplinas = new List<int>();

            if (usuario.Professor != null && usuario.Professor.Disciplinas != null)
                this.Disciplinas = usuario.Professor.Disciplinas.Select(e => e.Id).ToList();
        }
    }
}
