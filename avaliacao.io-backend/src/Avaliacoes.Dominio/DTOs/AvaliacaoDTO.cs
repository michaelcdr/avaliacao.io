using Avaliacoes.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.DTOs
{
    public class AvaliacaoDTO
    {
        public string Nota { get; set; }
        public string Disciplina { get; set; }
        public string Competencia { get; set; }
        public string Habilidade { get; set; }
        public string Dimensao { get; set; }
        public string Semestre { get; set; }
        public string Data { get; set; }
        public int DisciplinaId { get; set; }
        public int CompetenciaId { get; set; }
        public int HabilidadeId { get; set; }
        public int DimensaoId { get; set; }

        public AvaliacaoDTO()
        {

        }

        public AvaliacaoDTO(Avaliacao e)
        {
            Nota =  e.ObterNotaFormatada();
            Disciplina = e.Dimensao.Habilidade.Competencia.Disciplina.Nome;
            Competencia = e.Dimensao.Habilidade.Competencia.Nome;
            Habilidade = e.Dimensao.Habilidade.Nome;
            Dimensao = e.Dimensao.Nome;
            Semestre = e.Semestre;
            DisciplinaId = e.Dimensao.Habilidade.Competencia.DisciplinaId;
            CompetenciaId = e.Dimensao.Habilidade.CompetenciaId;
            HabilidadeId = e.Dimensao.Habilidade.Id;
            DimensaoId = e.DimensaoId;
            Data = e.DataAvaliacao.ToString("dd/MM/yyyy");
        }


    }
}
