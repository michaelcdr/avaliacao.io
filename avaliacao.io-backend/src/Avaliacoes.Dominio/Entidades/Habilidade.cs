using Avaliacoes.Dominio.DTOs;
using System;
using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Habilidade : EntidadeBase
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descritivo { get; private set; }
        public int CompetenciaId { get; private set; }
        public Competencia Competencia { get; private set; }
        public List<Dimensao> Dimensoes { get; private set; }

        public Habilidade()
        {
            this.Dimensoes = new List<Dimensao>();
        }

        public Habilidade(int competenciaId, string nome, string descritivo, List<Dimensao> dimensoes)
        {
            this.Dimensoes = dimensoes ?? new List<Dimensao>();
            this.CompetenciaId = competenciaId;
            this.Nome = nome;
            this.Descritivo = descritivo;
        }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this.AdicionarErro("Informe o nome.", "Nome");

            if (this.Dimensoes.Count != 3)
                this.AdicionarErro("Uma habilidade deve conter 3 dimensões","Dimensoes");

            return this._erros.Count == 0;
        }

        public HabilidadeDTO ToDTO()
        {
            return new HabilidadeDTO(this.Id, this.Nome, this.Descritivo, this.CompetenciaId, this.Dimensoes);
        }
    }
}
