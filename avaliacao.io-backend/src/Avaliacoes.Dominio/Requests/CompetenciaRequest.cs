namespace Avaliacoes.Dominio.Requests
{
    public class CompetenciaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public int DisciplinaId { get; set; }
    }
}
