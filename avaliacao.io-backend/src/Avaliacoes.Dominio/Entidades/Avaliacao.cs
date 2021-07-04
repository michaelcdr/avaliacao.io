using System;

namespace Avaliacoes.Dominio.Entidades
{
    public class Avaliacao : EntidadeBase
    {
        public int Id { get; set; }
        
        public int DimensaoId { get; set; }
        public Dimensao Dimensao { get; set; }

        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }

        public int Nota { get; set; }
        public string Semestre { get; set; }
        public DateTime DataAvaliacao { get; set; }

        public Avaliacao()
        {

        }

        public Avaliacao(int idDimensao, int alunoId, int nota, string semestre)
        {
            this.DimensaoId = idDimensao;
            this.AlunoId = alunoId;
            this.Nota = nota;
            this.Semestre = semestre;
            this.DataAvaliacao = DateTime.Now;
        }

        /// <summary>
        /// Aptidão plena =	2
        //  Aptidão =	    1
        //  Insuficiente =	0
        /// </summary>
        /// <returns></returns>
        public override bool TaValido()
        {
            if (Nota < 0 || Nota > 2)
                AdicionarErro("A nota deve ser maior ou igual a 0 e menor ou igual a 2.", "Nota");

            if (string.IsNullOrEmpty(Semestre))
                AdicionarErro("O semestre deve ser informado.", "Nota");

            return _erros.Count == 0;
        }

        public void AtualizarNota(int nota)
        {
            this.Nota = nota;
        }
    }
}
