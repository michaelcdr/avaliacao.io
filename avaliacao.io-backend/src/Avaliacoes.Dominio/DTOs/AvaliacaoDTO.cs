using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.DTOs
{
    public class AvaliacaoDTO
    {
        public string Nota { get; set; }
        public string Disciplina { get; set; }
        public string Competencia { get; set; }
        public string Habilidade { get; set; }
        public string Dimensao { get; set; }
        public string Semestre { get; set; }
        public string Data { get; set; }
        public int DisciplinaId { get; set; }
        public int CompetenciaId { get; set; }
        public int HabilidadeId { get; set; }
        public int DimensaoId { get; set; }
    }
}
