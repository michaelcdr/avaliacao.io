using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Habilidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public int CompetenciaId { get; set; }
        public Competencia Competencia { get; set; }
        public List<Dimensao> Dimensoes { get; set; }
    }
}
