using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Disciplina 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public string Horario { get; set; }
        public List<Professor> Professores { get; set; }
        //public List<Competencia> Competencias { get; set; }
        
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
            {
                this.Professores.Add(professor);
            }
        }

        public void AdicionarProfessor(Professor professor)
        {
            if (this.Professores == null)
                this.Professores = new List<Professor>();

            this.Professores.Add(professor);
        }
    }





    //public class Competencia
    //{
    //    public int Id { get; set; }
    //    public string Nome { get; set; }
    //    public string Descricao { get; set; }
    //    public Disciplina Disciplina { get; set; }
    //    public int DisciplinaId { get; set; }
    //    public List<Habilidade> Habilidades { get; set; }
    //}
    //public class Habilidade
    //{
    //    public int Id { get; set; }
    //    public string Nome { get; set; }
    //    public string Descricao { get; set; }
    //    public int CompetenciaId { get; set; }
    //    public Competencia Competencia { get; set; }
    //    public List<Dimensao> Dimensoes { get; set; }
    //}
    //public class Dimensao
    //{
    //    public int Id { get; set; }
    //    public string Nome { get; set; }
    //    public int Codigo { get; set; }
    //    public Habilidade Habilidade { get; set; }
    //    public int HabilidadeId { get; set; }
    //}
}
