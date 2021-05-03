namespace Avaliacoes.Dominio.Entidades
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }
        public Professor Professor { get; set; }
        //public Aluno Aluno { get; set; }
    }
}
