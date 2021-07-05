using Avaliacoes.Aplicacao.Helpers;
using Avaliacoes.Dominio.DTOs.Responses;
using Avaliacoes.Dominio.Requests;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Avaliacoes.Aplicacao.Clients
{
    public class AlunoApiClient
    {
        private readonly HttpClient httpClient;

        public AlunoApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private string BotarAspas(string valor) => $"\"{valor}\"";

        public async Task<AppResponse> Importar(ImportarAlunos request)
        {
            var content = new MultipartFormDataContent();

            var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(request.Arquivo));
            imagemBytes.Headers.Add("content-type", "application/vnd.ms-excel");

            content.Add(imagemBytes, BotarAspas("arquivo"), BotarAspas("excel-importacao.xlsx"));

            HttpResponseMessage resposta = await httpClient.PostAsync("Alunos/Importar", content);

            AppResponse conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppResponse>();

            return conteudoResposta;
        } 
    }
}
