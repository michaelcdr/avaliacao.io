using Microsoft.AspNetCore.Http;

namespace Avaliacoes.Dominio.Requests
{
    public class ImportarAlunos
    {
        public IFormFile Arquivo { get; set; }
    }
}
