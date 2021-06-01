namespace Avaliacoes.Dominio.DTOs
{
    public class CompenciaDTO
    {
        public CompenciaDTO(int id, string nome, string descritivo, int disciplinaId)
        {
            Id = id;
            Nome = nome;
            Descritivo = descritivo;
            DisciplinaId = disciplinaId;
        }

        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public string Descritivo { get; set; }
        public string Nome { get; set; }
    }
}
