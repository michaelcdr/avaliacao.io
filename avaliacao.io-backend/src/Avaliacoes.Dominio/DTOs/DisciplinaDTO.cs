using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.DTOs
{
    public class DisciplinaDTO
    {
        public string Nome { get; set; }
        public string Descritivo { get; set; }

        /// <summary>
        /// Lista de Ids dos Professores
        /// </summary>
        public List<int> Professores { get; set; }
    }

    public class ProfessorDTO
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public List<int> Disciplinas { get; set; }
    }

    public class ProfessorComDisciplinaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public List<int> Disciplinas { get; set; }
    }
}
