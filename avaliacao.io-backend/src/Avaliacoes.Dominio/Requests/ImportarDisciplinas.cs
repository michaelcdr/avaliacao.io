using Microsoft.AspNetCore.Http;

namespace Avaliacoes.Dominio.Requests
{
    public class ImportarDisciplinas
    {
        public IFormFile Arquivo { get; set; }
    }
}
