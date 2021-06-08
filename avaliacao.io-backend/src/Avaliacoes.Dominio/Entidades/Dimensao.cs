using Avaliacoes.Dominio.DTOs;

namespace Avaliacoes.Dominio.Entidades
{
    public class Dimensao:EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
        public Habilidade Habilidade { get; set; }
        public int HabilidadeId { get; set; }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(Nome))
                this.AdicionarErro("Informe o nome", "Nome");

            return _erros.Count == 0;
        }

        public DimensaoDTO ToDTO()
        {
            return new DimensaoDTO(this.Id,  this.HabilidadeId,  this.Nome,  this.Codigo);
        }
    }
}
