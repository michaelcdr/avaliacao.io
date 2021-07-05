using Avaliacoes.Aplicacao.Services.Interfaces;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Requests;
using Avaliacoes.Dominio.Transacoes;
using Microsoft.AspNetCore.Hosting;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Services
{
    public class DisciplinaService : IDisciplinaService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUnitOfWork _uow;

        public DisciplinaService(IUnitOfWork uow,   IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
            this._uow = uow;
        }

        public async Task<AppResponse> ImportarDisciplinas(ImportarDisciplinas importarAlunos)
        {
            var erros = new List<string>();

            if (importarAlunos.Arquivo.Length == 0)
                return new AppResponse(false, "Arquivo não informado ou corrompido.");
            else
            {
                var filePath = Path.GetTempFileName();
                string destino = Path.Combine(_hostingEnvironment.WebRootPath, "Importacoes", Guid.NewGuid() + ".xlsx");

                using (var stream = new FileStream(destino, FileMode.Create))
                    await importarAlunos.Arquivo.CopyToAsync(stream);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet;

                using (var stream = new FileStream(destino, FileMode.Open))
                {
                    stream.Position = 0;
                    XSSFWorkbook xssWorkbook = new XSSFWorkbook(stream);
                    sheet = xssWorkbook.GetSheetAt(0);
                }

                int linha = 0;
                var disciplinas = new List<Disciplina>();

                foreach (IRow row in sheet)
                {
                    if (linha == 0)
                    {
                        linha++;
                        continue;
                    }

                    string nome = row.GetCell(0).ToString();
                    string descritivo = row.GetCell(1).ToString();
                    string horario = row.GetCell(2).ToString();

                    var disciplina = new Disciplina(nome, descritivo, horario);
                    if (!disciplina.TaValido())
                        erros.Add(string.Join("\n", disciplina.ObterErros().Select(e => e.Mensagem).ToList()));
                    else
                        disciplinas.Add(disciplina);

                    linha++;
                }

                foreach (Disciplina disciplina in disciplinas)
                    _uow.Disciplinas.Add(disciplina);
                
                await _uow.CommitAsync();
            }

            if (erros.Count == 0)
                return new AppResponse(true, "Importação realizada com sucesso.");
            else
                return new AppResponse(true, $"Importação realizada, porem ocorreram alguns erros:\n {string.Join("\n", erros)}");
        }
    }
}
