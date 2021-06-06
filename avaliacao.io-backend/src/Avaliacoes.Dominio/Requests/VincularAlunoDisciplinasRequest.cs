using System.Collections.Generic;

namespace Avaliacoes.Dominio.Requests
{
    public class VincularAlunoDisciplinasRequest
    {
        public List<int> IdsDisciplinas { get; set; }
        public string UsuarioId { get; set; }

        public VincularAlunoDisciplinasRequest(List<int> idsDisciplinas, string usuarioId)
        {
            this.IdsDisciplinas = idsDisciplinas ?? new List<int>();
            this.UsuarioId = usuarioId;
        }
    }
}
