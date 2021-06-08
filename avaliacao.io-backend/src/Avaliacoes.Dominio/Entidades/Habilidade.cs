using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Validar();
        }

        public Habilidade(int competenciaId, string nome, string descritivo, List<Dimensao> dimensoes)
        {
            this.Dimensoes = dimensoes ?? new List<Dimensao>();
            this.CompetenciaId = competenciaId;
            this.Nome = nome;
            this.Descritivo = descritivo;

            Validar();
        }

        private void Validar()
        {
            this.LimparErros();

            if (string.IsNullOrEmpty(this.Nome))
                this.AdicionarErro("Informe o nome.", "Nome");

            if (this.Dimensoes.Count != 3)
                this.AdicionarErro("Uma habilidade deve conter 3 dimensões", "Dimensoes");

            var nomesSemRepeticoes = this.Dimensoes.Select(e => e.Nome).Distinct().ToList();

            if (this.Dimensoes.Count != nomesSemRepeticoes.Count)
                this.AdicionarErro("Não deve haver dimensões com o mesmo nome.", "");
        }

        public override bool TaValido()
        {
            Validar();

            return this._erros.Count == 0;
        }

        public HabilidadeDTO ToDTO()
        {
            return new HabilidadeDTO(this.Id, this.Nome, this.Descritivo, this.CompetenciaId, this.Dimensoes);
        }

        public void Atualizar(HabilidadeRequest request)
        {
            this.CompetenciaId = request.CompetenciaId;
            this.Descritivo = request.Descritivo;
            this.Nome = request.Nome;
            this.Dimensoes = request.Dimensoes.Select(item => new Dimensao { HabilidadeId = this.Id, Nome = item.Nome, Codigo = item.Codigo }).ToList() ?? new List<Dimensao>();
            
            Validar();
        }
    }
}
