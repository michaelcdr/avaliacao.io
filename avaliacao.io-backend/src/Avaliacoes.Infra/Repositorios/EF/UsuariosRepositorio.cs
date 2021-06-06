﻿using Avaliacoes.Dominio.Entidades;
using Avaliacoes.Dominio.Repositorios;
using Avaliacoes.Infra.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Avaliacoes.Infra.Repositorios.EF
{
    public class UsuariosRepositorio : Repositorio<Usuario>, IUsuariosRepositorio
    {
        private const string ROLENAME_PROFESSOR = "Professor";
        private const string ROLENAME_ALUNO = "Aluno";

        public UsuariosRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<List<Aluno>> ObterAlunosPorDisciplina(int idDisciplina)
        {
            var usuarios = await (from u in ApplicationDbContext.Usuarios
                                  join ru in ApplicationDbContext.UserRoles on u.Id equals ru.UserId
                                  join r in ApplicationDbContext.Roles on ru.RoleId equals r.Id
                                  where r.Name == ROLENAME_ALUNO && u.Aluno.Disciplinas.Any(disciplinaAtual => disciplinaAtual.Id == idDisciplina)
                                  select u).Include(e => e.Aluno).ThenInclude(e => e.Disciplinas).ToListAsync();

            var alunos = new List<Aluno>();

            if (usuarios != null)
                alunos = usuarios.Select(e => e.Aluno).Distinct().ToList();

            return alunos;
        }

        public async Task<List<Aluno>> ObterAlunos()
        {
            var usuarios = await (from u in ApplicationDbContext.Usuarios
                                  join ru in ApplicationDbContext.UserRoles on u.Id equals ru.UserId
                                  join r in ApplicationDbContext.Roles on ru.RoleId equals r.Id
                                  where r.Name == ROLENAME_ALUNO 
                                  select u).Include(e => e.Aluno).ThenInclude(e => e.Disciplinas).ToListAsync();

            var alunos = new List<Aluno>();

            if (usuarios != null)
                alunos = usuarios.Select(e => e.Aluno).Distinct().ToList();

            return alunos;
        }

        public async Task<List<Usuario>> ObterProfessores()
        {
            return await (from u in ApplicationDbContext.Usuarios
                          join ru in ApplicationDbContext.UserRoles on u.Id equals ru.UserId
                          join r in ApplicationDbContext.Roles on ru.RoleId equals r.Id
                          where r.Name  == ROLENAME_PROFESSOR
                          select u).Include(e => e.Professor).ThenInclude(e => e.Disciplinas).ToListAsync();
        }

        public async Task<Professor> ObterProfessor(string id)
        {
            Usuario usuario = await Obter(ROLENAME_PROFESSOR, id);
            return usuario.Professor;
        }

        public async Task<List<Professor>> ObterProfessores(List<string> idsUsuarios)
        {
            var usuarios = await (from u in ApplicationDbContext.Usuarios
                                 join ru in ApplicationDbContext.UserRoles on u.Id equals ru.UserId
                                 join r in ApplicationDbContext.Roles on ru.RoleId equals r.Id
                                 where r.Name == ROLENAME_PROFESSOR && idsUsuarios.Contains(u.Id)
                                 select u).Include(e => e.Professor).ThenInclude(e => e.Disciplinas).ToListAsync();

            var professores = new List<Professor>();

            if (usuarios != null)
                professores = usuarios.Select(e => e.Professor).Distinct().ToList();

            return professores;
        }

        public async Task<Usuario> Obter(string tipoUsuario, string usuarioId)
        {
            Usuario usuario = await (from u in ApplicationDbContext.Usuarios
                                     join ru in ApplicationDbContext.UserRoles on u.Id equals ru.UserId
                                     join r in ApplicationDbContext.Roles on ru.RoleId equals r.Id
                                     where r.Name == tipoUsuario && u.Id == usuarioId
                                     select u).Include(e => e.Professor).ThenInclude(e => e.Disciplinas)
                                              .Include(e => e.Aluno).ThenInclude(e => e.Disciplinas)
                                              .SingleOrDefaultAsync();

            return usuario;
        }

        public async Task<Aluno> ObterAluno(string usuarioId)
        {
            Usuario usuario = await Obter(ROLENAME_ALUNO, usuarioId);
            return usuario.Aluno; 
        }
    }
}
