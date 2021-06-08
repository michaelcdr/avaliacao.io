using Avaliacoes.Dominio.DTOs;
using Avaliacoes.Dominio.Requests;
using System;
using System.Collections.Generic;

namespace Avaliacoes.Dominio.Entidades
{
    public class Competencia : EntidadeBase
    {
        public Competencia(int disciplinaId, string nome, string descritivo)
        {
            DisciplinaId = disciplinaId;
            Nome = nome;
            Descritivo = descritivo;
        }
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descritivo { get; set; }
        public Disciplina Disciplina { get; set; }
        public int DisciplinaId { get; set; }
        public List<Habilidade> Habilidades { get; set; } = new List<Habilidade>();

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this.AdicionarErro("Informe o nome.", "Nome");

            if (this.DisciplinaId == 0)
                this.AdicionarErro("Informe a disciplina.", "DisciplinaId");

            return this._erros.Count == 0;
        }

        public CompenciaDTO ToDTO()
        {
            return new CompenciaDTO(this.Id,this.Nome, this.Descritivo, this.DisciplinaId);
        }

        public void Atualizar(CompetenciaRequest request)
        {
            this.Nome = request.Nome;
            this.Descritivo = request.Descritivo;
            this.DisciplinaId = request.DisciplinaId;
        }
    }
}
