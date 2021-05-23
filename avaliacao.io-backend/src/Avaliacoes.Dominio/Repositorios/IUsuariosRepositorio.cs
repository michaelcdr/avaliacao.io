using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface IUsuariosRepositorio : IRepositorio<Usuario>
    {
        Task<Professor> ObterProfessor(string id);
        Task<List<Usuario>> ObterProfessores();
        Task<List<Professor>> ObterProfessores(List<string> idsUsuarios);
        Task<Usuario> Obter(string idUsuario);
    }
}
