namespace Avaliacoes.Dominio.Entidades
{
    public class Coordenador 
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
