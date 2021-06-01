namespace Avaliacoes.Dominio.Entidades
{
    public class Dimensao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public Habilidade Habilidade { get; set; }
        public int HabilidadeId { get; set; }
    }
}
