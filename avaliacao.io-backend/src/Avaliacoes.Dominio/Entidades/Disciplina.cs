using System.Collections.Generic;
using System.Linq;

namespace Avaliacoes.Dominio.Entidades
{
    public class Disciplina 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }

        public List<Professor> Professores { get; set; }

        //public void VincularProfessores(List<Professor> professores)
        //{
        //    if (professores == null)
        //        AddErro("Uma disciplina deve conter professores");
        //    else
        //    {
        //        foreach (var item in professores)
        //            if (!professores.Any(e => e.Id == item.Id))
        //                this.Professores.Add(item);
        //    }
        //}

        public Disciplina(string  nome, string descritivo)
        {
            this.Nome = nome;
            this.Descritivo = descritivo;
        }
    }
}
