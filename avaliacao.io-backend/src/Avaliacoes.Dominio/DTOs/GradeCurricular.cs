using System.Collections.Generic;

namespace Avaliacoes.Dominio.DTOs
{
    public class GradeCurricular
    {
        public string UsuarioId { get; set; }
        public string NomeAluno { get; set; }

        public List<DisciplinaAvaliadaDTO> Disciplinas { get; set; }
    }

    public class DisciplinaAvaliadaDTO
    {
        public int DisciplinaId { get; set; }
        public string Nome { get; set; }
        public List<CompetenciaAvaliadaDTO> Competencias { get; set; }
    }

    public class CompetenciaAvaliadaDTO
    {
        public int CompentenciaId { get; set; }
        public string Nome { get; set; }
        public List<HabilidadeAvaliadaDTO> Habilidades { get; set; }
    }

    public class HabilidadeAvaliadaDTO
    {
        public int HabilidadeId { get; set; }
        public string Nome { get; set; }
        public List<DimensaoAvaliadaDTO> Dimensoes { get; set; }
    }

    public class DimensaoAvaliadaDTO
    {
        public int DimensaoId { get; set; }
        public int AvaliacaoId { get; set; }
        public string Nome { get; set; }
        public string Nota { get; set; }
        public string Semestre { get; set; }
        public string Data { get; set; }
    }
}
