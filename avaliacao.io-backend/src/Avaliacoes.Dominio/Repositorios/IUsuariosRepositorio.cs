using Avaliacoes.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Avaliacoes.Dominio.Repositorios
{
    public interface IUsuariosRepositorio : IRepositorio<Usuario>
    {
        Task<List<Usuario>> ObterProfessores();
        Task<List<Professor>> ObterProfessores(List<string> idsUsuarios);
        Task<Usuario> Obter(string tipoUsuario, string idUsuario);
        Task<List<Aluno>> ObterAlunosPorDisciplina(int idDisciplina);
        Task<List<Aluno>> ObterAlunos();
        Task<Aluno> ObterAluno(string usuarioId);
        Task<Coordenador> ObterCoordenador(string id);
        Task<Professor> ObterProfessor(string id);
        Task<List<Coordenador>> ObterCoordenadores();
    }
}
