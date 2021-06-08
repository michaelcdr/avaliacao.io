namespace Avaliacoes.Dominio.DTOs
{
    public class DimensaoDTO
    {
        public DimensaoDTO(int id, int habilidadeId, string nome, int codigo)
        {
            Id = id;
            HabilidadeId = habilidadeId;
            Nome = nome;
            Codigo = codigo;
        }

        public string Nome { get; set; }
        public int Codigo { get; set; }
        public int Id { get; set; }
        public int HabilidadeId { get; set; }
    }
}
